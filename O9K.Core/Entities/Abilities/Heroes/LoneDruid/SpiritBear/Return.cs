namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid.SpiritBear
{
    using Base;
    using Base.Components;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lone_druid_spirit_bear_return)]
    public class Return : ActiveAbility, IChanneled
    {
        public Return(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = false;
    }
}