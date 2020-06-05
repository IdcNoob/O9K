namespace O9K.Core.Entities.Abilities.Heroes.Puck
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.puck_phase_shift)]
    public class PhaseShift : ActiveAbility, IChanneled, IShield, IAppliesImmobility
    {
        private readonly SpecialData channelTimeData;

        public PhaseShift(Ability baseAbility)
            : base(baseAbility)
        {
            //todo chain stun calcs

            this.channelTimeData = new SpecialData(baseAbility, "duration");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public float ChannelTime
        {
            get
            {
                return this.channelTimeData.GetValue(this.Level);
            }
        }

        public string ImmobilityModifierName { get; } = "modifier_puck_phase_shift";

        public bool IsActivatesOnChannelStart { get; } = false;

        public string ShieldModifierName { get; } = "modifier_puck_phase_shift";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}