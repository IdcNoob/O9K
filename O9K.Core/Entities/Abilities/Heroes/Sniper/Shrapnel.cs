namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.sniper_shrapnel)]
    public class Shrapnel : CircleAbility, IDebuff
    {
        public Shrapnel(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "damage_delay");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_sniper_shrapnel_slow";
    }
}