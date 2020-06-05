namespace O9K.Core.Entities.Abilities.Heroes.DrowRanger
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId((AbilityId)343)]
    public class Multishot : ConeAbility, IChanneled, IHarass
    {
        public Multishot(Ability baseAbility)
            : base(baseAbility)
        {
            //todo better data
            this.RadiusData = new SpecialData(baseAbility, "arrow_width");
            this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public override float CastRange
        {
            get
            {
                return this.Owner.GetAttackRange() * 2f;
            }
        }

        public float ChannelTime { get; }

        public override float EndRadius { get; } = 400;

        public bool IsActivatesOnChannelStart { get; } = true;

        public override float Range
        {
            get
            {
                return this.Owner.GetAttackRange() * 2f;
            }
        }
    }
}