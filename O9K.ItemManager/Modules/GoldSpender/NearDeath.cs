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
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Utils;

    internal class NearDeath : IModule
    {
        private readonly MenuAbilityPriorityChanger abilityToggler;

        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly MenuSlider enemyDistance;

        private readonly MenuSlider hpPctThreshold;

        private readonly MenuSlider hpThreshold;

        private readonly AbilityId[] items =
        {
            AbilityId.item_tpscroll,
            AbilityId.attribute_bonus,
            //AbilityId.item_ward_observer,
            AbilityId.item_ward_sentry,
            AbilityId.item_tome_of_knowledge,
            AbilityId.item_dust,
            AbilityId.item_smoke_of_deceit,
            AbilityId.item_clarity,
            AbilityId.item_flask,
            AbilityId.item_tango,
            AbilityId.item_enchanted_mango,
        };

        private readonly MenuSlider saveForBuyback;

        private readonly Sleeper sleeper = new Sleeper();

        private Owner owner;

        [ImportingConstructor]
        public NearDeath(IContext9 context, IMainMenu mainMenu)
        {
            this.context = context;
            this.context.AssemblyEventManager.AssemblyLoaded();

            this.saveForBuyback = mainMenu.GoldSpenderMenu.GetOrAdd(new MenuSlider("Save for buyback", 30, 0, 60))
                .SetTooltip("Save for buyback after x mins");

            var menu = mainMenu.GoldSpenderMenu.Add(new Menu("Near death"));
            menu.AddTranslation(Lang.Ru, "Перед смертью");
            menu.AddTranslation(Lang.Cn, "接近死亡");

            this.enabled = menu.Add(new MenuSwitcher("Enabled", false)).SetTooltip("Buy items when your hero is about to die");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Автоматически покупать предметы, когда герой может умереть");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "在英雄快要死的时候买东西");

            this.hpThreshold = menu.Add(new MenuSlider("HP threshold", 150, 1, 500)).SetTooltip("Buy items if your hero has less HP");
            this.hpThreshold.AddTranslation(Lang.Ru, "Порог ХП");
            this.hpThreshold.AddTooltipTranslation(Lang.Ru, "Покупать предметы, когда у героя меньше здоровья");
            this.hpThreshold.AddTranslation(Lang.Cn, "生命值");
            this.hpThreshold.AddTooltipTranslation(Lang.Cn, "如果您的英雄的HP少则购买物品");

            this.hpPctThreshold = menu.Add(new MenuSlider("HP% threshold", 20, 1, 40)).SetTooltip("Buy items if your hero has less HP%");
            this.hpPctThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.hpPctThreshold.AddTooltipTranslation(Lang.Ru, "Покупать предметы, когда у героя меньше здоровья%");
            this.hpPctThreshold.AddTranslation(Lang.Cn, "生命值％");
            this.hpPctThreshold.AddTooltipTranslation(Lang.Cn, "如果英雄的HP低与％，则购买物品");

            this.enemyDistance = menu.Add(new MenuSlider("Enemy range", 600, 0, 2000))
                .SetTooltip("Buy items only if there are enemies in range");
            this.enemyDistance.AddTranslation(Lang.Ru, "Дистанция до врага");
            this.enemyDistance.AddTooltipTranslation(Lang.Ru, "Покупать предметы, если враг находится ближе");
            this.enemyDistance.AddTranslation(Lang.Cn, "敌人范围");
            this.enemyDistance.AddTooltipTranslation(Lang.Cn, "仅在范围内有敌人时才购买物品");

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
            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.context.AssemblyEventManager.EvaderPredictedDeath -= this.OnEvaderPredictedDeath;
            EntityManager9.UnitMonitor.UnitHealthChange -= this.OnUnitHealthChange;
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

            return ShopFlags.Base;
        }

        private static int GetItemCharges(IEnumerable<Ability9> currentItems, AbilityId itemId)
        {
            return (int)currentItems.Where(x => x.Id == itemId).Sum(x => x.BaseItem.CurrentCharges);
        }

        private static int GetWardCharges(Ability9[] currentItems, AbilityId itemId)
        {
            return (int)currentItems.Where(x => x.Id == AbilityId.item_ward_dispenser)
                       .Sum(x => itemId == AbilityId.item_ward_observer ? x.BaseItem.CurrentCharges : x.BaseItem.SecondaryCharges)
                   + (int)currentItems.Where(x => x.Id == itemId).Sum(x => x.BaseItem.CurrentCharges);
        }

        private void BuyItems()
        {
            try
            {
                if (Game.IsPaused || this.sleeper.IsSleeping)
                {
                    return;
                }

                if (!this.owner.Hero.IsAlive || this.owner.Hero.CanReincarnate)
                {
                    return;
                }

                var (reliableGold, unreliableGold) = this.owner.GetAvailableGold(this.saveForBuyback);
                var itemsToBuy = new List<(Unit9 unit, AbilityId abilityId)>();
                var currentItems = this.owner.Hero.Abilities.Where(x => x.IsItem).ToArray();

                foreach (var itemId in this.GetAllItems())
                {
                    if (unreliableGold <= 0)
                    {
                        break;
                    }

                    var unit = this.GetUnitToPurchase(itemId);
                    if (unit == null)
                    {
                        continue;
                    }

                    if (this.IsItemIgnored(currentItems, itemId))
                    {
                        continue;
                    }

                    var price = itemId.GetPrice();

                    if (reliableGold + unreliableGold < price)
                    {
                        continue;
                    }

                    itemsToBuy.Add((unit, itemId));
                    unreliableGold -= price;
                }

                if (itemsToBuy.Count == 0 || unreliableGold > this.owner.GetGoldLossOnDeath())
                {
                    return;
                }

                UpdateManager.BeginInvoke(
                    async () =>
                        {
                            try
                            {
                                foreach (var (unit, id) in itemsToBuy)
                                {
                                    Player.BuyItem(unit, id);
                                    await Task.Delay(50);
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.Error(e);
                            }
                        });

                this.sleeper.Sleep(20);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.AssemblyEventManager.EvaderPredictedDeath += this.OnEvaderPredictedDeath;
                EntityManager9.UnitMonitor.UnitHealthChange += this.OnUnitHealthChange;
            }
            else
            {
                this.context.AssemblyEventManager.EvaderPredictedDeath -= this.OnEvaderPredictedDeath;
                EntityManager9.UnitMonitor.UnitHealthChange -= this.OnUnitHealthChange;
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
                    list.AddRange(Player.QuickBuyItems);
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

            var flags = itemId.GetShopFlags();

            if ((this.GetUnitShopFlags(this.owner) & flags) != 0)
            {
                return this.owner;
            }

            var courier = EntityManager9.Units.FirstOrDefault(x => x.IsCourier && x.IsAlly(this.owner) && x.IsAlive);
            if (courier == null)
            {
                return null;
            }

            if ((this.GetUnitShopFlags(courier) & flags) != 0)
            {
                return courier;
            }

            return null;
        }

        private bool IsItemIgnored(Ability9[] currentItems, AbilityId itemId)
        {
            if (!itemId.CanBePurchased(this.owner.Team))
            {
                return true;
            }

            switch (itemId)
            {
                case AbilityId.item_clarity:
                case AbilityId.item_tango:
                case AbilityId.item_flask:
                case AbilityId.item_enchanted_mango:
                {
                    if (Game.GameTime > 10 * 60)
                    {
                        return true;
                    }

                    break;
                }
                case AbilityId.item_tpscroll:
                    if (GetItemCharges(currentItems, itemId) >= 2)
                    {
                        return true;
                    }

                    break;
                case AbilityId.item_dust:
                    if (GetItemCharges(currentItems, itemId) >= 2
                        || !EntityManager9.Abilities.Any(x => x.IsInvisibility && !x.Owner.IsAlly(this.owner)))
                    {
                        return true;
                    }

                    break;
                //   case AbilityId.item_ward_observer:
                case AbilityId.item_ward_sentry:
                    if (GetWardCharges(currentItems, itemId) >= 2)
                    {
                        return true;
                    }

                    break;
                case AbilityId.item_energy_booster:
                    if (currentItems.Any(x => x.Id == AbilityId.item_arcane_boots))
                    {
                        return true;
                    }

                    break;
                case AbilityId.item_smoke_of_deceit:
                    if (GetItemCharges(currentItems, itemId) >= 1)
                    {
                        return true;
                    }

                    break;
                case AbilityId.item_tome_of_knowledge:
                    if (this.owner.Hero.Level >= GameData.MaxHeroLevel)
                    {
                        return true;
                    }

                    break;
            }

            return false;
        }

        private void OnEvaderPredictedDeath(object sender, EventArgs e)
        {
            this.BuyItems();
        }

        private void OnUnitHealthChange(Unit9 unit, float health)
        {
            try
            {
                if (!unit.IsMyHero)
                {
                    return;
                }

                if (health > this.hpThreshold && ((health / unit.MaximumHealth) * 100) > this.hpPctThreshold)
                {
                    return;
                }

                if (!EntityManager9.Heroes.Any(x => !x.IsAlly(unit) && x.Distance(unit) < this.enemyDistance && x.IsAlive))
                {
                    return;
                }

                this.BuyItems();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}