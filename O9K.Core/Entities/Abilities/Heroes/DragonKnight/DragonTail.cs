namespace O9K.Core.Entities.Abilities.Heroes.DragonKnight
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.dragon_knight_dragon_tail)]
    public class DragonTail : RangedAbility, IDisable
    {
        private readonly SpecialData castRangeData;

        public DragonTail(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix cast range

            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.castRangeData = new SpecialData(baseAbility, "dragon_cast_range");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasModifier("modifier_dragon_knight_dragon_form"))
                {
                    return this.castRangeData.GetValue(this.Level);
                }

                return base.BaseCastRange + 100;
            }
        }
    }
}