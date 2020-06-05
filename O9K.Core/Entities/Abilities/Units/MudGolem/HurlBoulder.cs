namespace O9K.Core.Entities.Abilities.Units.MudGolem
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.mud_golem_hurl_boulder)]
    public class HurlBoulder : RangedAbility, IDisable
    {
        public HurlBoulder(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;
    }
}