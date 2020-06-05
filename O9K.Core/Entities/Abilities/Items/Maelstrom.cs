namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_maelstrom)]
    public class Maelstrom : PassiveAbility
    {
        public Maelstrom(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}