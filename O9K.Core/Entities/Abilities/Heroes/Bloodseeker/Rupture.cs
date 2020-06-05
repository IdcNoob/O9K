namespace O9K.Core.Entities.Abilities.Heroes.Bloodseeker
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bloodseeker_rupture)]
    public class Rupture : RangedAbility, IDebuff
    {
        private readonly SpecialData castRangeData;

        public Rupture(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRangeData = new SpecialData(baseAbility, "abilitycastrange");
        }

        public string DebuffModifierName { get; } = "modifier_bloodseeker_rupture";

        public override bool IsDisplayingCharges
        {
            get
            {
                return this.Owner.HasAghanimsScepter;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}