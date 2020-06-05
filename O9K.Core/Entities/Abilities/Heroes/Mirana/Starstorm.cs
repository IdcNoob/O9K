namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.mirana_starfall)]
    public class Starstorm : AreaOfEffectAbility, INuke
    {
        public Starstorm(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "starfall_radius");
        }
    }
}