namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.dark_seer_surge)]
    public class Surge : RangedAbility, IBuff
    {
        public Surge(Ability baseAbility)
            : base(baseAbility)
        {
            //todo buff radius fix
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_dark_seer_3);

                if (talent?.Level > 0)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public string BuffModifierName { get; } = "modifier_dark_seer_surge";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}