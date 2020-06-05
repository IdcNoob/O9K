namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_pirate_hat)]
    public class PirateHat : PassiveAbility
    {
        public PirateHat(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}