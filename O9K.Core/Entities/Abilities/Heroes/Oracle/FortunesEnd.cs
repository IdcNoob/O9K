namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.oracle_fortunes_end)]
    public class FortunesEnd : RangedAbility, IDisable, IChanneled, IAppliesImmobility
    {
        private readonly SpecialData aghanimsBonusCastRangeData;

        public FortunesEnd(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.aghanimsBonusCastRangeData = new SpecialData(baseAbility, "scepter_bonus_range");
            this.ChannelTime = baseAbility.GetChannelTime(0);
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public override float CastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return base.CastRange + this.aghanimsBonusCastRangeData.GetValue(this.Level);
                }

                return base.CastRange;
            }
        }

        public float ChannelTime { get; }

        public string ImmobilityModifierName { get; } = "modifier_oracle_fortunes_end_purge";

        public bool IsActivatesOnChannelStart { get; } = true;
    }
}