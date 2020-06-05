namespace O9K.ItemManager.Modules.Snatcher.Controllables
{
    using System.Linq;

    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Utils;

    internal class Controllable
    {
        public Controllable(Unit9 unit)
        {
            this.Unit = unit;
            this.Handle = unit.Handle;
        }

        public uint Handle { get; }

        public bool IsValid
        {
            get
            {
                return this.Unit.IsValid && this.Unit.IsAlive && !this.Sleeper.IsSleeping;
            }
        }

        protected Sleeper Sleeper { get; } = new Sleeper();

        protected Unit9 Unit { get; }

        public virtual bool CanPick(PhysicalItem physicalItem)
        {
            if (!this.ShouldPick(physicalItem))
            {
                return false;
            }

            if (physicalItem.Item.NeutralTierIndex >= 0)
            {
                if (this.Unit.BaseInventory.FreeBackpackSlots.Any() || this.Unit.BaseInventory.GetItem(ItemSlot.NeutralItemSlot) == null)
                {
                    return true;
                }
            }

            switch (physicalItem.Item.Id)
            {
                case AbilityId.item_gem:
                case AbilityId.item_rapier:
                case AbilityId.item_aegis:
                case AbilityId.item_refresher_shard:
                {
                    if (this.Unit.BaseInventory.FreeInventorySlots.Any())
                    {
                        return true;
                    }

                    if (!this.Unit.BaseInventory.FreeBackpackSlots.Any())
                    {
                        return false;
                    }

                    var item = this.Unit.BaseInventory.Items.OrderBy(x => x.Cost).FirstOrDefault(x => x.CanBeMovedToBackpack());
                    if (item == null)
                    {
                        return false;
                    }

                    item.MoveItem(this.Unit.BaseInventory.FreeBackpackSlots.First());
                    return true;
                }
                case AbilityId.item_cheese:
                case AbilityId.item_ultimate_scepter_2 when this.Unit.HasAghanimsScepter:
                {
                    return this.Unit.BaseInventory.FreeInventorySlots.Any() || this.Unit.BaseInventory.FreeBackpackSlots.Any();
                }
                case AbilityId.item_ultimate_scepter_2:
                {
                    return true;
                }
            }

            return false;
        }

        public virtual bool CanPick(Rune rune)
        {
            return this.ShouldPick(rune);
        }

        public void Pick(PhysicalItem item)
        {
            if (this.Unit.BaseUnit.PickUpItem(item))
            {
                if (item.Item.NeutralTierIndex >= 0 && !this.Unit.IsInvisible)
                {
                    UpdateManager.BeginInvoke(() => this.Unit.BaseUnit.Attack(this.Unit.Position, true), 100);
                }

                this.Sleeper.Sleep(0.5f);
            }
        }

        public void Pick(Rune rune)
        {
            if (this.Unit.BaseUnit.PickUpRune(rune))
            {
                this.Sleeper.Sleep(0.5f);
            }
        }

        protected bool ShouldPick(Entity entity)
        {
            return !this.Unit.IsCharging && !this.Unit.IsChanneling && this.Unit.Distance(entity.Position) < 400;
        }
    }
}