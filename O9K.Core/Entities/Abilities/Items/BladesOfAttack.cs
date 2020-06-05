namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_blades_of_attack)]
    public class BladesOfAttack : PassiveAbility
    {
        public BladesOfAttack(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}