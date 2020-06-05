namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    // [AbilityId(AbilityId.spectre_haunt_single)]
    [AbilityId((AbilityId)7851)]
    public class ShadowStep : RangedAbility, IHarass
    {
        public ShadowStep(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}