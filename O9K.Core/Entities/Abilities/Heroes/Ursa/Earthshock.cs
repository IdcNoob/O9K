namespace O9K.Core.Entities.Abilities.Heroes.Ursa
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ursa_earthshock)]
    public class Earthshock : AreaOfEffectAbility, INuke
    {
        private readonly SpecialData castRangeData;

        public Earthshock(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "hop_duration");
            this.RadiusData = new SpecialData(baseAbility, "shock_radius");
            this.castRangeData = new SpecialData(baseAbility, "hop_distance");
        }

        public override float CastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}