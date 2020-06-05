namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_javelin)]
    public class Javelin : PassiveAbility
    {
        public Javelin(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}