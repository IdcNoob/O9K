namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_meteor_hammer)]
    public class MeteorHammer : CircleAbility, IChanneled, IDisable
    {
        public MeteorHammer(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
            this.RadiusData = new SpecialData(baseAbility, "impact_radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "land_time");
        }

        public override float ActivationDelay
        {
            get
            {
                return base.ActivationDelay + this.ChannelTime;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float ChannelTime { get; }

        public override DamageType DamageType
        {
            get
            {
                return DamageType.Magical;
            }
        }

        public bool IsActivatesOnChannelStart { get; } = false;
    }
}