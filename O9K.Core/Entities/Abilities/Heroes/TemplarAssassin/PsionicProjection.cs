namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
    using Base;
    using Base.Components;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.templar_assassin_trap_teleport)]
    public class PsionicProjection : RangedAbility, IChanneled
    {
        public PsionicProjection(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = false;
    }
}