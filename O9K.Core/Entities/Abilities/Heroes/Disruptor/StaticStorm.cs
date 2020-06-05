namespace O9K.Core.Entities.Abilities.Heroes.Disruptor
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.disruptor_static_storm)]
    public class StaticStorm : CircleAbility, IDisable
    {
        public StaticStorm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public override float ActivationDelay { get; } = 0.25f; // hack dot delay for evader

        public UnitState AppliesUnitState
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return UnitState.Silenced | UnitState.Muted;
                }

                return UnitState.Silenced;
            }
        }
    }
}