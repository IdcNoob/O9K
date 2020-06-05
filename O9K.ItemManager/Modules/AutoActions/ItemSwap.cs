namespace O9K.ItemManager.Modules.AutoActions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Heroes;
    using Core.Entities.Units;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using SharpDX;

    internal class ItemSwap : IModule
    {
        private readonly MenuSwitcher consumableItems;

        private readonly HashSet<AbilityId> items = new HashSet<AbilityId>
        {
            AbilityId.item_aegis,
            AbilityId.item_cheese,
            AbilityId.item_moon_shard,
            AbilityId.item_refresher_shard,
            AbilityId.item_ultimate_scepter,
            AbilityId.item_enchanted_mango,
            AbilityId.item_clarity,
            AbilityId.item_flask,
            AbilityId.item_tango,
            AbilityId.item_tango_single,
            AbilityId.item_ward_observer,
            AbilityId.item_ward_sentry,
            AbilityId.item_dust,
            AbilityId.item_tome_of_knowledge,
            AbilityId.item_smoke_of_deceit,
            AbilityId.item_branches,
            AbilityId.item_infused_raindrop,
            AbilityId.item_faerie_fire
        };

        private readonly HashSet<AbilityId> itemsNeutral = new HashSet<AbilityId>
        {
            AbilityId.item_mango_tree,
            AbilityId.item_royal_jelly,
            AbilityId.item_repair_kit,
            AbilityId.item_greater_faerie_fire
        };

        private readonly MenuSwitcher neutralItems;

        private readonly MenuSwitcher philosophersStone;

        private bool moveStoneToStash;

        private Owner owner;

        private Item swapBackItem;

        [ImportingConstructor]
        public ItemSwap(IMainMenu mainMenu)
        {
            var menu = mainMenu.AutoActionsMenu.Add(new Menu("Item auto swap"));
            menu.AddTranslation(Lang.Ru, "Автоматическое перекладывание предметов");
            menu.AddTranslation(Lang.Cn, "物品的自动转移");

            this.consumableItems = menu.Add(new MenuSwitcher("Consumable items"))
                .SetTooltip("Take items from backpack when consumable used");
            this.consumableItems.AddTranslation(Lang.Ru, "Расходуемые предметы");
            this.consumableItems.AddTooltipTranslation(Lang.Ru, "Брать вещи из рюкзака при использовании расходуемых предметов");
            this.consumableItems.AddTranslation(Lang.Cn, "消耗品");
            this.consumableItems.AddTooltipTranslation(Lang.Cn, "使用消耗品时从背包中取出物品");

            this.neutralItems = menu.Add(new MenuSwitcher("Neutral items"))
                .SetTooltip("Take neutral items from backpack when used/transferred");
            this.neutralItems.AddTranslation(Lang.Ru, "Нейтральные предметы");
            this.consumableItems.AddTooltipTranslation(Lang.Ru, "Брать вещи из рюкзака при использовании/передаче");
            this.neutralItems.AddTranslation(Lang.Cn, "中立物品");
            this.consumableItems.AddTooltipTranslation(Lang.Cn, "使用/转移时从背包中拿东西");

            this.philosophersStone = menu.Add(new MenuSwitcher(LocalizationHelper.LocalizeName(AbilityId.item_philosophers_stone), "stone"))
                .SetTooltip("Take item on death");
            this.philosophersStone.AddTooltipTranslation(Lang.Ru, "Взять предмет при смерти");
            this.philosophersStone.AddTooltipTranslation(Lang.Cn, "死亡时取物品");
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;
            this.consumableItems.ValueChange += this.ConsumableItemsOnValueChange;
            this.neutralItems.ValueChange += this.NeutralItemsOnValueChange;
            this.philosophersStone.ValueChange += this.PhilosophersStoneOnValueChange;
        }

        public void Dispose()
        {
            this.neutralItems.ValueChange -= this.NeutralItemsOnValueChange;
            this.consumableItems.ValueChange -= this.ConsumableItemsOnValueChange;
            this.philosophersStone.ValueChange -= this.PhilosophersStoneOnValueChange;
            EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            EntityManager9.AbilityRemoved -= this.OnNeutralAbilityRemoved;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
            EntityManager9.UnitMonitor.UnitDied -= this.UnitMonitorOnUnitDied;
        }

        private void ConsumableItemsOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
            }
            else
            {
                EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
            }
        }

        private void MoveItemToNeutralStash(Item item)
        {
            try
            {
                if (!this.moveStoneToStash)
                {
                    return;
                }

                this.moveStoneToStash = false;
                Player.EntitiesOrder(OrderId.DropItemAtFountain, new[] { this.owner.Hero.BaseHero }, 0, item.Index, Vector3.Zero, false);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void NeutralItemsOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                Player.OnExecuteOrder += this.OnExecuteOrder;
                EntityManager9.AbilityRemoved += this.OnNeutralAbilityRemoved;
            }
            else
            {
                Player.OnExecuteOrder -= this.OnExecuteOrder;
                EntityManager9.AbilityRemoved -= this.OnNeutralAbilityRemoved;
            }
        }

        private void OnAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (ability.Owner != this.owner.Hero || !this.items.Contains(ability.Id))
                {
                    return;
                }

                UpdateManager.BeginInvoke(this.SwapItem, 545);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.IsPlayerInput || args.OrderId != OrderId.DropItemAtFountain)
                {
                    return;
                }

                UpdateManager.BeginInvoke(this.SwapNeutralItem, 589);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnNeutralAbilityRemoved(Ability9 ability)
        {
            try
            {
                if (ability.Owner != this.owner.Hero || !this.itemsNeutral.Contains(ability.Id))
                {
                    return;
                }

                UpdateManager.BeginInvoke(this.SwapNeutralItem, 511);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdateStone()
        {
            try
            {
                var hero = this.owner.Hero;
                if (hero?.IsAlive != true)
                {
                    return;
                }

                var stone = hero.BaseInventory.NeutralItem;
                if (stone?.Id != AbilityId.item_philosophers_stone)
                {
                    this.swapBackItem = null;
                    this.moveStoneToStash = false;
                    UpdateManager.Unsubscribe(this.OnUpdateStone);
                    return;
                }

                UpdateManager.BeginInvoke(() => this.MoveItemToNeutralStash(stone), 125);
                UpdateManager.BeginInvoke(this.SwapOldNeutralItem, 355);

                UpdateManager.Unsubscribe(this.OnUpdateStone);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void PhilosophersStoneOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.UnitMonitor.UnitDied += this.UnitMonitorOnUnitDied;
            }
            else
            {
                EntityManager9.UnitMonitor.UnitDied -= this.UnitMonitorOnUnitDied;
            }
        }

        private void SwapItem()
        {
            try
            {
                var inventory = this.owner.Hero.BaseHero.Inventory;
                if (!inventory.FreeInventorySlots.Any())
                {
                    return;
                }

                var backPackItem = inventory.Backpack.Where(x => x.NeutralTierIndex < 0 && !x.IsRecipe)
                    .OrderByDescending(x => x.Cost)
                    .FirstOrDefault();

                if (backPackItem == null)
                {
                    return;
                }

                backPackItem.MoveItem(inventory.FreeInventorySlots.First());
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void SwapNeutralItem()
        {
            try
            {
                var neutralItemInSlot = this.owner.Hero.BaseHero.Inventory.GetItem(ItemSlot.NeutralItemSlot);
                if (neutralItemInSlot != null)
                {
                    return;
                }

                var neutralItem = this.owner.Hero.BaseHero.Inventory.Backpack.FirstOrDefault(x => x.NeutralTierIndex >= 0);
                if (neutralItem == null)
                {
                    return;
                }

                neutralItem.MoveItem(ItemSlot.NeutralItemSlot);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void SwapOldNeutralItem()
        {
            try
            {
                if (this.swapBackItem == null)
                {
                    return;
                }

                var inventory = this.owner.Hero.BaseInventory;

                for (var i = ItemSlot.BackPack_1; i <= ItemSlot.StashSlot_6; i++)
                {
                    var slotItem = inventory.GetItem(i);
                    if (slotItem?.Handle == this.swapBackItem.Handle)
                    {
                        Player.MoveItem(this.owner.Hero, this.swapBackItem, ItemSlot.NeutralItemSlot);
                        break;
                    }
                }

                this.swapBackItem = null;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void TakeItemFromBackpack(Item stone)
        {
            try
            {
                var inventory = this.owner.Hero.BaseInventory;
                var neutralItem = inventory.NeutralItem;

                if (neutralItem?.Handle != stone.Handle)
                {
                    this.swapBackItem = neutralItem;
                    Player.MoveItem(this.owner.Hero, stone, ItemSlot.NeutralItemSlot);
                }

                UpdateManager.Subscribe(this.OnUpdateStone, 100);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void TakeItemFromStash(Item stone)
        {
            try
            {
                var inventory = this.owner.Hero.BaseInventory;
                var neutralItem = inventory.GetItem(ItemSlot.NeutralItemSlot);
                var freeSlots = inventory.FreeBackpackSlots.Concat(inventory.FreeStashSlots);
                if (!freeSlots.Any() && neutralItem != null)
                {
                    return;
                }

                this.moveStoneToStash = true;

                Player.EntitiesOrder(
                    OrderId.TakeItemFromNeutralItemStash,
                    new[] { this.owner.Hero.BaseHero },
                    0,
                    stone.Index,
                    Vector3.Zero,
                    false);

                UpdateManager.BeginInvoke(() => this.TakeItemFromBackpack(stone), 500);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void UnitMonitorOnUnitDied(Unit9 unit)
        {
            try
            {
                if (!unit.IsMyHero || unit.BaseInventory.NeutralItem?.Id == AbilityId.item_philosophers_stone)
                {
                    return;
                }

                var stone = ObjectManager.GetEntities<Item>()
                    .FirstOrDefault(x => x.Id == AbilityId.item_philosophers_stone && x.Team == this.owner.Team);

                if (stone == null || !(stone.Owner is Hero stoneOwner))
                {
                    return;
                }

                for (var i = ItemSlot.BackPack_1; i <= ItemSlot.NeutralItemSlot; i++)
                {
                    var slotItem = stoneOwner.Inventory.GetItem(i);
                    if (slotItem?.Handle == stone.Handle)
                    {
                        if (stoneOwner.Handle == unit.Handle)
                        {
                            UpdateManager.BeginInvoke(() => this.TakeItemFromBackpack(stone), 500);
                        }

                        return;
                    }
                }

                UpdateManager.BeginInvoke(() => this.TakeItemFromStash(stone), 500);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}