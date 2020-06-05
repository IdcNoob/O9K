namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    //todo id
    [AbilityId((AbilityId)7230)]
    public class CallOfTheWildBoar : ActiveAbility, IBuff
    {
        public CallOfTheWildBoar(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = string.Empty;

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}