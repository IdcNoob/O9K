namespace O9K.ItemManager.Modules.Snatcher.Controllables
{
    using System.Linq;

    using Core.Entities.Units;

    using Ensage;

    internal class SpiritBear : Controllable
    {
        public SpiritBear(Unit9 unit)
            : base(unit)
        {
        }

        public override bool CanPick(PhysicalItem physicalItem)
        {
            if (!this.ShouldPick(physicalItem))
            {
                return false;
            }

            switch (physicalItem.Item.Id)
            {
                case AbilityId.item_gem:
                case AbilityId.item_rapier:
                {
                    return this.Unit.BaseInventory.FreeInventorySlots.Any();
                }
                case AbilityId.item_aegis:
                {
                    return false;
                }
                case AbilityId.item_refresher_shard:
                case AbilityId.item_ultimate_scepter_2:
                case AbilityId.item_cheese:
                {
                    return this.Unit.BaseInventory.FreeInventorySlots.Any() || this.Unit.BaseInventory.FreeBackpackSlots.Any();
                }
            }

            return false;
        }
    }
}