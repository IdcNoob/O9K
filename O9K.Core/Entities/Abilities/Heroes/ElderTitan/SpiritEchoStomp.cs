namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.elder_titan_echo_stomp_spirit)]
    public class SpiritEchoStomp : AreaOfEffectAbility, IDisable
    {
        public SpiritEchoStomp(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "cast_time");
            this.DamageData = new SpecialData(baseAbility, "stomp_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return base.CanBeCasted(checkChanneling) && this.Owner.HasModifier("modifier_elder_titan_ancestral_spirit");
        }
    }
}