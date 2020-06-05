namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
    using System;

    using Base;
    using Base.Components;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.keeper_of_the_light_illuminate)]
    public class Illuminate : LineAbility, IChanneled
    {
        private readonly SpecialData channelTimeData;

        public Illuminate(Ability baseAbility)
            : base(baseAbility)
        {
            //todo spirit form illuminate
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.channelTimeData = new SpecialData(baseAbility, (Func<uint, float>)baseAbility.GetChannelTime);
        }

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