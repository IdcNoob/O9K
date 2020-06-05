namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.furion_sprout)]
    public class Sprout : RangedAbility, IDisable
    {
        public Sprout(Ability baseAbility)
            : base(baseAbility)
        {
            this.DurationData = new SpecialData(baseAbility, "duration");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Rooted;

        public override float Radius
        {
            get
            {
                return 200;
            }
        }
    }
}