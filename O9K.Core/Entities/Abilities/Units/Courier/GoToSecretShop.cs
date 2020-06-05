namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_go_to_secretshop)]
    public class GoToSecretShop : ActiveAbility
    {
        public GoToSecretShop(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}