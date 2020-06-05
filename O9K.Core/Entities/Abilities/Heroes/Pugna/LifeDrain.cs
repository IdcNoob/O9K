namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
    using Base;
    using Base.Components;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.pugna_life_drain)]
    public class LifeDrain : RangedAbility, IChanneled
    {
        public LifeDrain(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}