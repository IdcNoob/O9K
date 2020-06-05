namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.legion_commander_press_the_attack)]
    public class PressTheAttack : RangedAbility, IBuff, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public PressTheAttack(Ability baseAbility)
            : base(baseAbility)
        {
            this.DurationData = new SpecialData(baseAbility, "duration");
            this.healthRestoreData = new SpecialData(baseAbility, "hp_regen");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_legion_commander_5);

                if (talent?.Level > 0)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public string BuffModifierName { get; } = "modifier_legion_commander_press_the_attack";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_legion_commander_press_the_attack";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)(this.healthRestoreData.GetValue(this.Level) * this.Duration);
        }
    }
}