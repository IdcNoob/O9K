namespace O9K.Core.Entities.Abilities.Heroes.Alchemist
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.alchemist_acid_spray)]
    public class AcidSpray : CircleAbility, IDebuff
    {
        public AcidSpray(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_alchemist_acid_spray";
    }
}