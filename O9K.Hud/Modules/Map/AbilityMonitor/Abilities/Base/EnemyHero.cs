namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
    using System.Linq;

    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Extensions;

    internal class EnemyHero
    {
        private const AbilityId DispenserId = AbilityId.item_ward_dispenser;

        private const AbilityId ObserverId = AbilityId.item_ward_observer;

        private const AbilityId SentryId = AbilityId.item_ward_sentry;

        public EnemyHero(Unit9 unit)
        {
            this.Unit = unit;
            //this.ObserversCount = this.CountWards(ObserverId);
            //this.SentryCount = this.CountWards(SentryId);
        }

        public uint ObserversCount { get; set; }

        public uint SentryCount { get; set; }

        public Unit9 Unit { get; }

        public uint CountWards(AbilityId id)
        {
            var items = this.Unit.BaseInventory.Items.Concat(this.Unit.BaseInventory.Backpack).ToList();

            return (uint)(items.Where(x => x.Id == id).Sum(x => x.CurrentCharges) + items.Where(x => x.Id == DispenserId)
                              .Sum(x => id == AbilityId.item_ward_observer ? x.CurrentCharges : x.SecondaryCharges));
        }

        public bool DroppedWard(AbilityId id)
        {
            return ObjectManager.GetEntitiesFast<PhysicalItem>()
                .Any(x => (x.Item.Id == id || x.Item.Id == DispenserId) && x.Distance2D(this.Unit.Position) < 300);
        }

        public uint GetWardsCount(AbilityId id)
        {
            return id == ObserverId ? this.ObserversCount : this.SentryCount;
        }

        public void SetWardsCount(AbilityId id, uint count)
        {
            if (id == ObserverId)
            {
                this.ObserversCount = count;
            }
            else
            {
                this.SentryCount = count;
            }
        }
    }
}