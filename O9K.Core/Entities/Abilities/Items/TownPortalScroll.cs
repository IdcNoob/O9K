namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_tpscroll)]
    public class TownPortalScroll : RangedAbility, IChanneled
    {
        public TownPortalScroll(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public override float CastRange { get; } = 9999999;

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = false;
    }
}