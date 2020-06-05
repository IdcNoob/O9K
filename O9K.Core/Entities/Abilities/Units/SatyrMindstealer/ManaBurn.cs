namespace O9K.Core.Entities.Abilities.Units.SatyrMindstealer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.satyr_soulstealer_mana_burn)]
    public class ManaBurn : RangedAbility, IHarass
    {
        public ManaBurn(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}