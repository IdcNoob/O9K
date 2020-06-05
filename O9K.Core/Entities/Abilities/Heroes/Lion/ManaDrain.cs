namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
    using Base;
    using Base.Components;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lion_mana_drain)]
    public class ManaDrain : RangedAbility, IChanneled
    {
        public ManaDrain(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}