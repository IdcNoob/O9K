namespace O9K.Core.Entities.Abilities.Heroes.NyxAssassin
{
    using Base;
    using Base.Types;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.nyx_assassin_impale)]
    public class Impale : LineAbility, IDisable, INuke
    {
        public Impale(Ability baseAbility)
            : base(baseAbility)
        {
            //todo improve cast range

            this.RadiusData = new SpecialData(baseAbility, "width");
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "impale_damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        protected override float BaseCastRange
        {
            get
            {
                if (this.Owner.HasModifier("modifier_nyx_assassin_burrow"))
                {
                    var ability = this.Owner.GetAbilityById(AbilityId.nyx_assassin_burrow);
                    return ability.GetAbilitySpecialData("impale_burrow_range_tooltip");
                }

                return base.BaseCastRange;
            }
        }
    }
}