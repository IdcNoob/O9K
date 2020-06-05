namespace O9K.Core.Entities.Abilities.Heroes.DeathProphet
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.death_prophet_carrion_swarm)]
    public class CryptSwarm : ConeAbility, INuke
    {
        public CryptSwarm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "start_radius");
            this.EndRadiusData = new SpecialData(baseAbility, "end_radius");
            this.RangeData = new SpecialData(baseAbility, "range");
            this.SpeedData = new SpecialData(baseAbility, "speed");
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