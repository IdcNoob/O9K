namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_hand_of_midas)]
    public class HandOfMidas : RangedAbility
    {
        public HandOfMidas(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool TargetsEnemy { get; } = false;
    }
}