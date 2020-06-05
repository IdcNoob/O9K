namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
    using Base;
    using Base.Components;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.tiny_tree_channel)]
    public class TreeVolley : CircleAbility, IChanneled
    {
        public TreeVolley(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "splash_radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}