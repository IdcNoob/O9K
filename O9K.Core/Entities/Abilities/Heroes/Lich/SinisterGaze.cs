namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.lich_sinister_gaze)]
    public class SinisterGaze : RangedAbility, IDisable, IChanneled, IAppliesImmobility
    {
        private readonly SpecialData channelTimeData;

        public SinisterGaze(Ability baseAbility)
            : base(baseAbility)
        {
            //todo radius
            this.channelTimeData = new SpecialData(baseAbility, "duration");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior = (behavior & ~AbilityBehavior.UnitTarget) | AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public float ChannelTime
        {
            get
            {
                return this.channelTimeData.GetValue(this.Level);
            }
        }

        public string ImmobilityModifierName { get; } = "modifier_lich_sinister_gaze";

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}