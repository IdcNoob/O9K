namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rattletrap_rocket_flare)]
    public class RocketFlare : CircleAbility, INuke
    {
        public RocketFlare(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public override float CastRange { get; } = 9999999;
    }
}