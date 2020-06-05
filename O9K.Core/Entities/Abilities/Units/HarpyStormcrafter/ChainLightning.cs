namespace O9K.Core.Entities.Abilities.Units.HarpyStormcrafter
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.harpy_storm_chain_lightning)]
    public class ChainLightning : RangedAbility, INuke
    {
        public ChainLightning(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "initial_damage");
        }
    }
}