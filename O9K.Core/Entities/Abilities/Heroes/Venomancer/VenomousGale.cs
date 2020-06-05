namespace O9K.Core.Entities.Abilities.Heroes.Venomancer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.venomancer_venomous_gale)]
    public class VenomousGale : LineAbility, IDebuff
    {
        public VenomousGale(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "strike_damage");
        }

        public string DebuffModifierName { get; } = "modifier_venomancer_venomous_gale";
    }
}