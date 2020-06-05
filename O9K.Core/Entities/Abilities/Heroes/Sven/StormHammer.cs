namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.sven_storm_bolt)]
    public class StormHammer : RangedAreaOfEffectAbility, IDisable, INuke
    {
        private readonly SpecialData scepterBonusCastRangeData;

        public StormHammer(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "bolt_aoe");
            this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
            this.scepterBonusCastRangeData = new SpecialData(baseAbility, "cast_range_bonus_scepter");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float CastRange
        {
            get
            {
                var range = base.CastRange;

                if (this.Owner.HasAghanimsScepter)
                {
                    range += this.scepterBonusCastRangeData.GetValue(this.Level);
                }

                return range;
            }
        }

        public bool IsDispelActive
        {
            get
            {
                //todo change
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_sven_3);
                if (talent?.Level > 0)
                {
                    return true;
                }

                return false;
            }
        }
    }
}