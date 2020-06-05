namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.brewmaster_earth_spell_immunity)]
    public class SpellImmunity : PassiveAbility
    {
        public SpellImmunity(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}