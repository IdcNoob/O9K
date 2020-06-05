namespace O9K.Core.Entities.Abilities.Heroes.CentaurWarrunner
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.centaur_return)]
    public class Retaliate : ActiveAbility, IBuff
    {
        public Retaliate(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_centaur_return_bonus_damage";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}