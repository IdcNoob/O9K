namespace O9K.Core.Entities.Abilities.Heroes.Bristleback
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bristleback_viscous_nasal_goo)]
    public class ViscousNasalGoo : RangedAbility, IDebuff
    {
        private readonly SpecialData scepterCastRange;

        public ViscousNasalGoo(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix aoe prediction
            this.scepterCastRange = new SpecialData(baseAbility, "radius_scepter");
            this.SpeedData = new SpecialData(baseAbility, "goo_speed");
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

        public override float CastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterCastRange.GetValue(this.Level) - 100;
                }

                return base.CastRange;
            }
        }

        public string DebuffModifierName { get; } = string.Empty; //"modifier_bristleback_viscous_nasal_goo"
    }
}