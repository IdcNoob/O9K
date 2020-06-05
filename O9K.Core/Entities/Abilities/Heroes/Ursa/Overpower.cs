namespace O9K.Core.Entities.Abilities.Heroes.Ursa
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.ursa_overpower)]
    public class Overpower : ActiveAbility, IBuff
    {
        public Overpower(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_ursa_overpower";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}