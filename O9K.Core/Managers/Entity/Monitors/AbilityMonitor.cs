namespace O9K.Core.Managers.Entity.Monitors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Entities.Abilities.Base;
    using Entities.Abilities.Base.Components;
    using Entities.Units;

    using Logger;

    public sealed class AbilityMonitor : IDisposable
    {
        private readonly HashSet<string> canUseOwnerItems = new HashSet<string>
        {
            "npc_dota_lone_druid_bear1",
            "npc_dota_lone_druid_bear2",
            "npc_dota_lone_druid_bear3",
            "npc_dota_lone_druid_bear4"
        };

        private readonly ItemSlot[] inventoryItemSlots =
        {
            ItemSlot.InventorySlot_1,
            ItemSlot.InventorySlot_2,
            ItemSlot.InventorySlot_3,
            ItemSlot.InventorySlot_4,
            ItemSlot.InventorySlot_5,
            ItemSlot.InventorySlot_6,
            ItemSlot.TPSlot,
            ItemSlot.NeutralItemSlot
        };

        private readonly ItemSlot[] stashItemSlots =
        {
            ItemSlot.BackPack_1,
            ItemSlot.BackPack_2,
            ItemSlot.BackPack_3,
            ItemSlot.StashSlot_1,
            ItemSlot.StashSlot_2,
            ItemSlot.StashSlot_3,
            ItemSlot.StashSlot_4,
            ItemSlot.StashSlot_5,
            ItemSlot.StashSlot_6
        };

        public AbilityMonitor()
        {
            Entity.OnBoolPropertyChange += this.OnBoolPropertyChange;
            Entity.OnFloatPropertyChange += this.OnFloatPropertyChange;
          //  Entity.OnInt32PropertyChange += this.OnInt32PropertyChange;
            ObjectManager.OnAddEntity += OnAddEntity;
            ObjectManager.OnRemoveEntity += OnRemoveEntity;

            UpdateManager.Subscribe(this.GameOnUpdate2, 500);
            Game.OnUpdate += this.GameOnUpdate;
        }

        private void GameOnUpdate2()
        {
            //hack
            try
            {
                foreach (var unit in EntityManager9.Units.Where(x => x.IsVisible))
                {
                    var inventory = unit.BaseUnit?.Inventory;
                    if (inventory == null)
                    {
                        continue;
                    }

                    var checkedItems = new List<uint>();

                    foreach (var handle in this.GetInventoryItems(inventory).Select(x => x.Handle))
                    {
                        var item = EntityManager9.GetAbilityFast(handle);
                        if (item == null)
                        {
                            continue;
                        }

                        checkedItems.Add(handle);

                        if (item.Owner == unit)
                        {
                            item.IsAvailable = true;
                            continue;
                        }

                        EntityManager9.RemoveAbility(item.BaseAbility);
                        EntityManager9.AddAbility(item.BaseAbility);
                    }

                    
                    foreach (var handle in this.GetStashItems(inventory).Select(x => x.Handle))
                    {
                        var item = EntityManager9.GetAbilityFast(handle);
                        if (item == null)
                        {
                            continue;
                        }

                        checkedItems.Add(handle);

                        if (item.Owner == unit)
                        {
                            item.IsAvailable = false;
                            continue;
                        }

                        EntityManager9.RemoveAbility(item.BaseAbility);
                        EntityManager9.AddAbility(item.BaseAbility);
                    }

                    // stashed neutral items
                    foreach (var item in unit.AbilitiesFast.Where(
                        x => x.IsItem && x.IsAvailable && !checkedItems.Contains(x.Handle)))
                    {
                        item.IsAvailable = false;
                    }

                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private readonly HashSet<uint> abilitiesInPhase = new HashSet<uint>();

        private void GameOnUpdate(EventArgs args)
        {
            //hack
            try
            {
                foreach (var ability in EntityManager9.Abilities.OfType<ActiveAbility>())
                {
                    if (!ability.Owner.IsAlive || !ability.Owner.IsVisible)
                    {
                        continue;
                    }

                    if (ability.BaseAbility.IsInAbilityPhase)
                    {
                        if (this.abilitiesInPhase.Add(ability.Handle))
                        {
                            ability.Owner.IsCasting = ability.IsCasting = true;
                            this.AbilityCastChange?.Invoke(ability);
                        }
                    }
                    else
                    {
                        if (this.abilitiesInPhase.Remove(ability.Handle))
                        {
                            ability.Owner.IsCasting = ability.IsCasting = false;
                            this.AbilityCastChange?.Invoke(ability);
                        }
                    }
                }

            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public delegate void EventHandler(Ability9 ability);

        public event EventHandler AbilityCastChange;

        public event EventHandler AbilityCasted;

        public event EventHandler AbilityChannel;

        public void Dispose()
        {
            Entity.OnBoolPropertyChange -= this.OnBoolPropertyChange;
            Entity.OnFloatPropertyChange -= this.OnFloatPropertyChange;
            Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
            ObjectManager.OnAddEntity -= OnAddEntity;
            ObjectManager.OnRemoveEntity -= OnRemoveEntity;
        }

        internal void SetOwner(Ability9 ability, Unit9 owner)
        {
            if (!ability.IsItem)
            {
                ability.SetOwner(owner);
                return;
            }

            var item = ability.BaseItem;
            var itemPurchaser = item.Purchaser?.Hero;

            if (item.Shareability == Shareability.Full || owner.IsIllusion || itemPurchaser?.IsValid != true || owner.Owner?.IsValid != true
                || (this.canUseOwnerItems.Contains(owner.Name) && itemPurchaser.Handle == owner.Owner.Handle))
            {
                ability.SetOwner(owner);
            }
            else
            {
                ability.SetOwner(EntityManager9.AddUnit(itemPurchaser));
            }

            this.UpdateItemState(ability);
        }

        private static void OnAddEntity(EntityEventArgs args)
        {
            try
            {
                var physicalItem = args.Entity as PhysicalItem;
                if (physicalItem == null || physicalItem.Item == null)
                {
                    return;
                }

                var item = EntityManager9.GetAbilityFast(physicalItem.Item.Handle);
                if (item == null)
                {
                    return;
                }

                item.IsAvailable = false;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private static void OnRemoveEntity(EntityEventArgs args)
        {
            try
            {
                var physicalItem = args.Entity as PhysicalItem;
                if (physicalItem == null || physicalItem.Item == null)
                {
                    return;
                }

                var item = EntityManager9.GetAbilityFast(physicalItem.Item.Handle);
                if (item == null)
                {
                    return;
                }

                item.IsAvailable = true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private IEnumerable<Item> GetInventoryItems(Inventory inventory)
        {
            foreach (var slot in this.inventoryItemSlots)
            {
                var item = inventory.GetItem(slot);

                if (item != null)
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<Item> GetStashItems(Inventory inventory)
        {
            foreach (var slot in this.stashItemSlots)
            {
                var item = inventory.GetItem(slot);

                if (item != null)
                {
                    yield return item;
                }
            }
        }

        private void OnBoolPropertyChange(Entity sender, BoolPropertyChangeEventArgs args)
        {
            var newValue = args.NewValue;
            if (newValue == args.OldValue)
            {
                return;
            }

            try
            {
                switch (args.PropertyName)
                {
                    case "m_bToggleState":
                    {
                        if (!newValue)
                        {
                            break;
                        }

                        var ability = EntityManager9.GetAbilityFast(sender.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        this.AbilityCasted?.Invoke(ability);
                        break;
                    }
                    //case "m_bInAbilityPhase":
                    //{
                    //    var ability = EntityManager9.GetAbilityFast(sender.Handle);
                    //    if (ability == null)
                    //    {
                    //        return;
                    //    }

                    //    ability.IsCasting = newValue;
                    //    ability.Owner.IsCasting = newValue;

                    //    this.AbilityCastChange?.Invoke(ability);
                    //    break;
                    //}
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnFloatPropertyChange(Entity sender, FloatPropertyChangeEventArgs args)
        {
            var newValue = args.NewValue;
            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (newValue == args.OldValue)
            {
                return;
            }

            try
            {
                switch (args.PropertyName)
                {
                    case "m_flEnableTime":
                    {
                        var ability = EntityManager9.GetAbilityFast(sender.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        ability.ItemEnableTimeSleeper.SleepUntil(newValue);
                        break;
                    }
                    case "m_flCastStartTime":
                    {
                        if (this.AbilityCasted == null)
                        {
                            break;
                        }

                        var ability = EntityManager9.GetAbilityFast(sender.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (!ability.IsDisplayingCharges)
                        {
                            return;
                        }

                        var castTime = newValue - args.OldValue;
                        if (castTime < 0 || args.OldValue < 0)
                        {
                            return;
                        }

                        var visibleTime = Game.RawGameTime - ability.Owner.LastNotVisibleTime;
                        if (visibleTime < 0.05f)
                        {
                            return;
                        }

                        if (ability.CastPoint <= 0)
                        {
                            this.AbilityCasted(ability);
                        }
                        else
                        {
                            if (Math.Abs(ability.CastPoint - castTime) < 0.03f)
                            {
                                this.AbilityCasted(ability);
                            }
                        }

                        break;
                    }
                    case "m_fCooldown":
                    {
                        if (this.AbilityCasted == null)
                        {
                            break;
                        }

                        if (newValue <= args.OldValue || args.OldValue > 0)
                        {
                            break;
                        }

                        var ability = EntityManager9.GetAbilityFast(sender.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        var visibleTime = Game.RawGameTime - ability.Owner.LastNotVisibleTime;
                        if (visibleTime < 0.05f)
                        {
                            return;
                        }

                        this.AbilityCasted(ability);
                        break;
                    }
                    case "m_flChannelStartTime":
                    {
                        var ability = EntityManager9.GetAbilityFast(sender.Handle);
                        if (ability == null)
                        {
                            return;
                        }

                        if (!(ability is IChanneled channeled))
                        {
                            break;
                        }

                        if (newValue > 0)
                        {
                            ability.IsChanneling = true;
                            channeled.Owner.ChannelEndTime = newValue + channeled.ChannelTime;
                            channeled.Owner.ChannelActivatesOnCast = channeled.IsActivatesOnChannelStart;

                            this.AbilityChannel?.Invoke(ability);
                        }
                        else
                        {
                            ability.IsChanneling = false;
                            channeled.Owner.ChannelEndTime = 0;
                            channeled.Owner.ChannelActivatesOnCast = false;
                        }

                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            if (args.NewValue == args.OldValue || args.PropertyName != "m_iParity")
            {
                return;
            }

            var owner = EntityManager9.GetUnitFast(sender.Handle);
            if (owner == null)
            {
                return;
            }

            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            var inventory = owner.BaseInventory;
                            var checkedItems = new List<uint>();

                            foreach (var handle in this.GetInventoryItems(inventory).Select(x => x.Handle))
                            {
                                var item = EntityManager9.GetAbilityFast(handle);
                                if (item == null)
                                {
                                    continue;
                                }

                                checkedItems.Add(handle);

                                if (item.Owner == owner)
                                {
                                    item.IsAvailable = true;
                                    continue;
                                }

                                EntityManager9.RemoveAbility(item.BaseAbility);
                                EntityManager9.AddAbility(item.BaseAbility);
                            }

                            foreach (var handle in this.GetStashItems(inventory).Select(x => x.Handle))
                            {
                                var item = EntityManager9.GetAbilityFast(handle);
                                if (item == null)
                                {
                                    continue;
                                }

                                checkedItems.Add(handle);

                                if (item.Owner == owner)
                                {
                                    item.IsAvailable = false;
                                    continue;
                                }

                                EntityManager9.RemoveAbility(item.BaseAbility);
                                EntityManager9.AddAbility(item.BaseAbility);
                            }

                            // stashed neutral items
                            foreach (var item in owner.AbilitiesFast.Where(
                                x => x.IsItem && x.IsAvailable && !checkedItems.Contains(x.Handle)))
                            {
                                item.IsAvailable = false;
                            }
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    });
        }

        private void UpdateItemState(Ability9 ability)
        {
            UpdateManager.BeginInvoke(
                () =>
                    {
                        try
                        {
                            var owner = ability.Owner;
                            if (owner?.IsValid != true)
                            {
                                return;
                            }

                            ability.IsAvailable = this.GetInventoryItems(owner.BaseInventory).Any(x => x.Handle == ability.Handle);
                        }
                        catch (Exception e)
                        {
                            Logger.Error(e);
                        }
                    },
                500);
        }
    }
}