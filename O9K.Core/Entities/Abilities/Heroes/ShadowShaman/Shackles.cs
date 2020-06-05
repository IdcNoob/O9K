namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.shadow_shaman_shackles)]
    public class Shackles : RangedAbility, IChanneled, IDisable
    {
        private readonly SpecialData channelTimeData;

        public Shackles(Ability baseAbility)
            : base(baseAbility)
        {
            this.channelTimeData = new SpecialData(baseAbility, "channel_time");
            this.DamageData = new SpecialData(baseAbility, "total_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float ChannelTime
        {
            get
            {
                return this.channelTimeData.GetValue(this.Level);
            }
        }

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}