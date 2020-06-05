namespace O9K.Core.Entities.Abilities.Units.SatyrTormenter
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.satyr_hellcaller_shockwave)]
    public class Shockwave : ConeAbility, INuke
    {
        public Shockwave(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.RadiusData = new SpecialData(baseAbility, "radius_start");
            this.EndRadiusData = new SpecialData(baseAbility, "radius_end");
            this.RangeData = new SpecialData(baseAbility, "distance");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                // disable casting on target
                return base.AbilityBehavior & ~AbilityBehavior.UnitTarget;
            }
        }
    }
}