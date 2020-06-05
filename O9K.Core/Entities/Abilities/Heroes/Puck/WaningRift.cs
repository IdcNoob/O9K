namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.puck_waning_rift)]
    public class WaningRift : CircleAbility, IDisable, INuke
    {
        private readonly SpecialData castRangeData;

        public WaningRift(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.castRangeData = new SpecialData(baseAbility, "max_distance");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Silenced;

        public override float CastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}