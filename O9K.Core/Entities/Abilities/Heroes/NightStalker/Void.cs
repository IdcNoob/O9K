namespace O9K.Core.Entities.Abilities.Heroes.NightStalker
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.night_stalker_void)]
    public class Void : RangedAbility, INuke, IDebuff, IDisable
    {
        private readonly SpecialData scepterCastRange;

        public Void(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix aoe prediction
            this.scepterCastRange = new SpecialData(baseAbility, "radius_scepter");
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

        public UnitState AppliesUnitState
        {
            get
            {
                if (Game.IsNight)
                {
                    return UnitState.Stunned;
                }

                return 0;
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

        public string DebuffModifierName { get; } = "modifier_night_stalker_void";
    }
}