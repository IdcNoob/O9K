namespace O9K.Core.Entities.Abilities.Heroes.Warlock
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.warlock_shadow_word)]
    public class ShadowWord : RangedAbility, IHealthRestore, IDebuff
    {
        public ShadowWord(Ability baseAbility)
            : base(baseAbility)
        {
            //todo better talent aoe calcs
            this.DurationData = new SpecialData(baseAbility, "duration");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_warlock_6);

                if (talent?.Level > 0)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public string DebuffModifierName { get; } = "modifier_warlock_shadow_word";

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_warlock_shadow_word";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.GetRawDamage(unit)[this.DamageType] * this.Duration);
        }
    }
}