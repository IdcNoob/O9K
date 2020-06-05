namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.treant_living_armor)]
    public class LivingArmor : RangedAbility, IHealthRestore
    {
        private readonly SpecialData healthRestoreData;

        public LivingArmor(Ability baseAbility)
            : base(baseAbility)
        {
            this.DurationData = new SpecialData(baseAbility, "duration");
            this.healthRestoreData = new SpecialData(baseAbility, "total_heal");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_treant_7);

                if (talent?.Level > 0)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public override float CastRange { get; } = 9999999;

        public bool InstantRestore { get; } = false;

        public string RestoreModifierName { get; } = "modifier_treant_living_armor";

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.healthRestoreData.GetValue(this.Level);
        }
    }
}