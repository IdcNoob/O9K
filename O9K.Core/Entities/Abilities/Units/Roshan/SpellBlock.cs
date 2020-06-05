namespace O9K.Core.Entities.Abilities.Units.Roshan
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.roshan_spell_block)]
    public class SpellBlock : PassiveAbility
    {
        public SpellBlock(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}