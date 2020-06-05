namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.spectre_spectral_dagger)]
    public class SpectralDagger : RangedAbility, INuke
    {
        public SpectralDagger(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}