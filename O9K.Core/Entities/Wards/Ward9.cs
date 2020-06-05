namespace O9K.Core.Entities.Wards
{
    using Ensage;

    using Metadata;

    using Units;

    [UnitName("npc_dota_observer_wards")]
    [UnitName("npc_dota_sentry_wards")]
    public class Ward9 : Unit9
    {
        public Ward9(Unit baseUnit)
            : base(baseUnit)
        {
            if (this.Name == "npc_dota_observer_wards")
            {
                this.IsObserverWard = true;
            }
            else
            {
                this.IsSentryWard = true;
            }

            this.IsUnit = false;
        }

        public bool IsObserverWard { get; }

        public bool IsSentryWard { get; }
    }
}