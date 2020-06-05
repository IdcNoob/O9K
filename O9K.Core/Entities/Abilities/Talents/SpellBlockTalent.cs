namespace O9K.Core.Entities.Abilities.Talents
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.special_bonus_spell_block_15)]
    internal class SpellBlockTalent : Talent
    {
        private readonly SpecialData spellBlockCooldownData;

        public SpellBlockTalent(Ability baseAbility)
            : base(baseAbility)
        {
            this.spellBlockCooldownData = new SpecialData(baseAbility, "block_cooldown");
        }

        public float SpellBlockCooldown
        {
            get
            {
                return this.spellBlockCooldownData.GetValue(1);
            }
        }
    }
}