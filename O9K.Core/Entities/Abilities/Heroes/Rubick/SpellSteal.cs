namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rubick_spell_steal)]
    public class SpellSteal : RangedAbility
    {
        private readonly SpecialData scepterCastRangeData;

        public SpellSteal(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.scepterCastRangeData = new SpecialData(baseAbility, "cast_range_scepter");
        }

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterCastRangeData.GetValue(this.Level);
                }

                return base.BaseCastRange;
            }
        }
    }
}