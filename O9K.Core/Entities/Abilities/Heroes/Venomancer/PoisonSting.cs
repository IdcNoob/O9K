namespace O9K.Core.Entities.Abilities.Heroes.Venomancer
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.venomancer_poison_sting)]
    public class PoisonSting : PassiveAbility
    {
        public PoisonSting(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}