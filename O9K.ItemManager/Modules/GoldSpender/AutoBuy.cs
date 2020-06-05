namespace O9K.ItemManager.Modules.GoldSpender
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;

    using Core.Data;
    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Utils;

    internal class AutoBuy : IModule
    {
        private readonly MenuAbilityPriorityChanger abilityToggler;

        private readonly MenuSwitcher enabled;

        private readonly AbilityId[] items =
        {
            AbilityId.item_tome_of_knowledge, AbilityId.attribute_bonus,
            //AbilityId.item_ward_observer
        };

        private readonly MenuSwitcher nearShop;

        //  private readonly MenuSwitcher prioritizeSideShop;

        private readonly MenuSlider saveForBuyback;

        private readonly Sleeper sleeper = new Sleeper();

        private readonly MenuSwitcher strictOrder;

        private readonly Sleeper tomeSleeper = new Sleeper();

        private Owner owner;

        private float tomeReuseCooldown;

        [ImportingConstructor]
        public AutoBuy(IMainMenu mainMenu)
        {
            this.saveForBuyback = mainMenu.GoldSpenderMenu.GetOrAdd(new MenuSlider("Save for buyback", 30, 0, 60))
                .SetTooltip("Save for buyback after x mins");
            this.saveForBuyback.AddTranslation(Lang.Ru, "Сохранять на байбэк");
            this.saveForBuyback.AddTooltipTranslation(Lang.Ru, "Сохранять на байбэк после х минут");
            this.saveForBuyback.AddTranslation(Lang.Cn, "购买状态保存");
            this.saveForBuyback.AddTooltipTranslation(Lang.Cn, "保存在X分钟后购买");

            var menu = mainMenu.GoldSpenderMenu.Add(new Menu("Auto buy"));
            menu.AddTranslation(Lang.Ru, "Авто покупка");
            menu.AddTranslation(Lang.Cn, "自动购买");

            this.enabled = menu.Add(new MenuSwitcher("Enabled", false)).SetTooltip("Auto buy items when you have enough gold");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Автоматически покупать предметы когда хватает золота");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "当钱充足时自动购买");

            this.nearShop = menu.Add(new MenuSwitcher("Buy only near shop")).SetTooltip("Buy items only near shop");
            this.nearShop.AddTranslation(Lang.Ru, "Только возле магазина");
            this.nearShop.AddTooltipTranslation(Lang.Ru, "Покупать предметы только когда герой возле магазина");
            this.nearShop.AddTranslation(Lang.Cn, "只在商店附近购买");
            this.nearShop.AddTooltipTranslation(Lang.Cn, "仅在野店购买物品");

            //this.prioritizeSideShop = menu.Add(new MenuSwitcher("Prioritize side shop"))
            //    .SetTooltip("Items that available in the side shop will not be purchased by courier in the base");
            //this.prioritizeSideShop.AddTranslation(Lang.Ru, "Приоритет бокового магазина");
            //this.prioritizeSideShop.AddTooltipTranslation(
            //    Lang.Ru,
            //    "Предметы которые есть в боковом магазине, не будут покупаться курьером на базе");
            //this.prioritizeSideShop.AddTranslation(Lang.Cn, "野店优先");
            //this.prioritizeSideShop.AddTooltipTranslation(Lang.Cn, "基地中的信使将在商店购买物品");

            this.strictOrder = menu.Add(new MenuSwitcher("Quick buy in strict order", false))
                .SetTooltip("Buy items from quick buy in order they were added");
            this.strictOrder.AddTranslation(Lang.Ru, "Строгий порядок");
            this.strictOrder.AddTooltipTranslation(
                Lang.Ru,
                "Предметы из быстрой покупки будут покупаться в том же порядке, что и были добавлены");
            this.strictOrder.AddTranslation(Lang.Cn, "严格订单");
            this.strictOrder.AddTooltipTranslation(Lang.Cn, "快速购买中的项目将以与添加的顺序相同的顺序购买");

            this.abilityToggler = menu.Add(
                new MenuAbilityPriorityChanger("Items", this.items.ToDictionary(x => x, _ => false), false, true).SetTooltip(
                    "Plus icon is for quick buy"));
            this.abilityToggler.AddTranslation(Lang.Ru, "Предметы");
            this.abilityToggler.AddTooltipTranslation(Lang.Ru, "Иконка плюса для быстрой покупки");
            this.abilityToggler.AddTranslation(Lang.Cn, "物品");
            this.abilityToggler.AddTooltipTranslation(Lang.Cn, "更多的图标用于快速购买");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;
            this.tomeReuseCooldown = new SpecialData(AbilityId.item_tome_of_knowledge, "AbilityCooldown").GetValue(1) + 30;

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            Game.OnFireEvent -= this.OnFireEvent;
            Entity.OnFloatPropertyChange -= this.OnFloatPropertyChange;
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        public ShopFlags GetUnitShopFlags(Unit9 unit)
        {
            switch (unit.BaseUnit.ActiveShop)
            {
                case ShopType.Base:
                    return ShopFlags.Base;
                case ShopType.Side:
                    return ShopFlags.Side;
                case ShopType.Secret:
                    return ShopFlags.Secret;
            }

            return ShopFlags.None;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdate, 1000);
                Game.OnFireEvent += this.OnFireEvent;
                Entity.OnFloatPropertyChange += this.OnFloatPropertyChange;
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
                Game.OnFireEvent -= this.OnFireEvent;
                Entity.OnFloatPropertyChange -= this.OnFloatPropertyChange;
            }
        }

        private IEnumerable<AbilityId> GetAllItems()
        {
            var list = new List<AbilityId>();

            foreach (var item in this.items.Where(x => this.abilityToggler.IsEnabled(x.ToString()))
                .OrderBy(x => this.abilityToggler.GetPriority(x.ToString())))
            {
                if (item == AbilityId.attribute_bonus)
                {
                    if (this.strictOrder)
                    {
                        list.AddRange(Player.QuickBuyItems);
                    }
                    else
                    {
                        var quickBuy = Player.QuickBuyItems.ToArray();
                        list.AddRange(quickBuy.Where(x => !x.IsRecipe() || quickBuy.All(z => z.IsRecipe())));
                    }

                    continue;
                }

                if (item == AbilityId.item_tome_of_knowledge && this.tomeSleeper)
                {
                    continue;
                }

                list.Add(item);
            }

            return list;
        }

        private Unit9 GetUnitToPurchase(AbilityId itemId)
        {
            if (Game.GameMode == GameMode.Turbo)
            {
                return this.owner;
            }

            var itemFlags = itemId.GetShopFlags();
            var ownerShopFlags = this.GetUnitShopFlags(this.owner);

            //if (this.prioritizeSideShop && ownerShopFlags == ShopFlags.None && (itemFlags & ShopFlags.Side) != 0)
            //{
            //    return null;
            //}

            if (!this.nearShop)
            {
                ownerShopFlags |= ShopFlags.Base;
            }

            if ((ownerShopFlags & itemFlags) != 0)
            {
                return this.owner;
            }

            if (this.nearShop)
            {
                return null;
            }

            var courier = EntityManager9.Units.FirstOrDefault(x => x.IsCourier && x.IsAlly(this.owner) && x.IsAlive);
            if (courier == null)
            {
                return null;
            }

            if ((this.GetUnitShopFlags(courier) & itemFlags) != 0)
            {
                return courier;
            }

            return null;
        }

        private bool IsItemIgnored(IEnumerable<Ability9> currentItems, AbilityId itemId)
        {
            if (!itemId.CanBePurchased(this.owner.Team))
            {
                return true;
            }

            switch (itemId)
            {
                case AbilityId.item_energy_booster:
                {
                    if (currentItems.Any(x => x.Id == AbilityId.item_arcane_boots))
                    {
                        return true;
                    }

                    break;
                }
                case AbilityId.item_tome_of_knowledge:
                {
                    if (this.owner.Hero.Level >= GameData.MaxHeroLevel)
                    {
                        return true;
                    }

                    break;
                }
            }

            return false;
        }

        private void OnFireEvent(FireEventEventArgs args)
        {
            if (args.GameEvent.Name != "dota_player_shop_changed")
            {
                return;
            }

            this.OnUpdate();
        }

        private void OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            if (this.tomeSleeper.IsSleeping || args.PropertyName != "fStockTime")
            {
                return;
            }

            this.OnUpdate();
        }

        private void OnUpdate()
        {
            try
            {
                if (Game.IsPaused || this.sleeper.IsSleeping)
                {
                    return;
                }

                var hero = this.owner.Hero;
                if (hero == null || !hero.IsValid)
                {
                    return;
                }

                var isAlive = hero.IsAlive;
                var (reliableGold, unreliableGold) = this.owner.GetAvailableGold(this.saveForBuyback);
                var itemsToBuy = new List<(Unit9 unit, AbilityId abilityId)>();
                var currentItems = hero.Abilities.Where(x => x.IsItem).ToArray();

                foreach (var itemId in this.GetAllItems())
                {
                    var unit = this.GetUnitToPurchase(itemId);
                    if (unit == null)
                    {
                        if (this.strictOrder)
                        {
                            break;
                        }

                        continue;
                    }

                    if (this.IsItemIgnored(currentItems, itemId))
                    {
                        continue;
                    }

                    var price = itemId.GetPrice();

                    if (reliableGold + unreliableGold >= price)
                    {
                        itemsToBuy.Add((unit, itemId));
                        unreliableGold -= price;
                    }
                    else if (this.strictOrder)
                    {
                        break;
                    }
                }

                if (itemsToBuy.Count == 0)
                {
                    return;
                }

                this.sleeper.Sleep(0.75f);

                UpdateManager.BeginInvoke(
                    async () =>
                        {
                            try
                            {
                                var inventory = hero.BaseInventory;
                                var maxItems = inventory.FreeBackpackSlots.Count() + inventory.FreeInventorySlots.Count();
                                var flags = this.GetUnitShopFlags(this.owner);

                                if ((!this.nearShop && flags == ShopFlags.None) || flags == ShopFlags.Base)
                                {
                                    maxItems += inventory.FreeStashSlots.Count();
                                }

                                foreach (var (unit, id) in itemsToBuy.Take(maxItems))
                                {
                                    if (id == AbilityId.item_tome_of_knowledge)
                                    {
                                        this.tomeSleeper.Sleep(this.tomeReuseCooldown);
                                    }
                                    else if (!isAlive)
                                    {
                                        continue;
                                    }

                                    Player.BuyItem(unit, id);
                                    await Task.Delay(50);
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e);
                            }
                        });
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}