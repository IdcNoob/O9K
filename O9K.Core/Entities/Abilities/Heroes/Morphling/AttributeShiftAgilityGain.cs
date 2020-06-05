namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.morphling_morph_agi)]
    public class AttributeShiftAgilityGain : ToggleAbility
    {
        public AttributeShiftAgilityGain(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override bool CanBeCastedWhileChanneling { get; } = true;
    }
}