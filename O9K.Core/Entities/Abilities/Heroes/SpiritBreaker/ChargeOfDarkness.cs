namespace O9K.Core.Entities.Abilities.Heroes.SpiritBreaker
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.spirit_breaker_charge_of_darkness)]
    public class ChargeOfDarkness : RangedAbility
    {
        public ChargeOfDarkness(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "movement_speed");
            this.RadiusData = new SpecialData(baseAbility, "bash_radius");
        }

        public override float CastRange { get; } = 9999999;

        public override float Speed
        {
            get
            {
                if (this.Owner.IsCharging && this.Owner.IsVisible)
                {
                    return this.Owner.Speed;
                }

                return this.Owner.Speed + this.SpeedData.GetValue(this.Level);
            }
        }
    }
}