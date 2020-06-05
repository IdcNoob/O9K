namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.brewmaster_earth_hurl_boulder)]
    public class HurlBoulder : RangedAbility, IDisable
    {
        public HurlBoulder(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.SpeedData = new SpecialData(baseAbility, "speed");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}