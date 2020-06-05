namespace O9K.Core.Entities.Abilities.Heroes.StormSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.storm_spirit_electric_vortex)]
    public class ElectricVortex : RangedAbility, IDisable
    {
        public ElectricVortex(Ability baseAbility)
            : base(baseAbility)
        {
            //todo better aghanim calcs
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.NoTarget;
                }

                return behavior;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}