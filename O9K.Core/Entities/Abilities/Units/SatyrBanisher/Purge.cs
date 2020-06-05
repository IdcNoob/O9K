namespace O9K.Core.Entities.Abilities.Units.SatyrBanisher
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.satyr_trickster_purge)]
    public class Purge : RangedAbility, IDebuff
    {
        public Purge(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string DebuffModifierName { get; } = "modifier_satyr_trickster_purge";
    }
}