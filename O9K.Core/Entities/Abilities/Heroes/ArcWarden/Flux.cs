namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.arc_warden_flux)]
    public class Flux : RangedAbility, IDebuff
    {
        private readonly SpecialData castRangeData;

        public Flux(Ability baseAbility)
            : base(baseAbility)
        {
            this.castRangeData = new SpecialData(baseAbility, "abilitycastrange");
        }

        public string DebuffModifierName { get; } = string.Empty; // debuff stacks

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }
    }
}