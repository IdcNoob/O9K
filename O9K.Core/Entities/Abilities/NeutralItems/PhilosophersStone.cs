namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_philosophers_stone)]
    public class PhilosophersStone : PassiveAbility
    {
        public PhilosophersStone(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}