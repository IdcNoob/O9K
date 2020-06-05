namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.ember_spirit_activate_fire_remnant)]
    public class ActivateFireRemnant : RangedAbility, IBlink
    {
        public ActivateFireRemnant(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public BlinkType BlinkType { get; } = BlinkType.Targetable;

        public override float CastRange { get; } = 9999999;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.Owner.HasModifier("modifier_ember_spirit_fire_remnant_timer");
        }
    }
}