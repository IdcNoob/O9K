namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.silencer_last_word)]
    public class LastWord : RangedAbility, IDebuff
    {
        public LastWord(Ability baseAbility)
            : base(baseAbility)
        {
            //todo radius
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public string DebuffModifierName { get; } = "modifier_silencer_last_word";
    }
}