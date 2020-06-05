namespace O9K.Core.Entities.Abilities.Units.Necronomicon.Warrior
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.necronomicon_warrior_mana_burn)]
    public class ManaBreak : PassiveAbility
    {
        public ManaBreak(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}