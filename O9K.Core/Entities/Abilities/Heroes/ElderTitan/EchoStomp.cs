namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.elder_titan_echo_stomp)]
    public class EchoStomp : AreaOfEffectAbility, IChanneled, IDisable, IAppliesImmobility
    {
        public EchoStomp(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "cast_time");
            this.DamageData = new SpecialData(baseAbility, "stomp_damage");
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public override float ActivationDelay
        {
            get
            {
                return base.ActivationDelay - this.CastPoint;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float ChannelTime { get; }

        public string ImmobilityModifierName { get; } = "modifier_elder_titan_echo_stomp";

        public bool IsActivatesOnChannelStart { get; } = false;
    }
}