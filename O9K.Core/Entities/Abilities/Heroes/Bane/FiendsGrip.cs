namespace O9K.Core.Entities.Abilities.Heroes.Bane
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bane_fiends_grip)]
    public class FiendsGrip : RangedAbility, IChanneled, IDisable
    {
        private readonly SpecialData channelTimeData;

        public FiendsGrip(Ability baseAbility)
            : base(baseAbility)
        {
            this.channelTimeData = new SpecialData(baseAbility, "abilitychanneltime");
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