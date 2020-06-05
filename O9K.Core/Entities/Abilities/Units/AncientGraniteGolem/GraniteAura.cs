namespace O9K.Core.Entities.Abilities.Units.AncientGraniteGolem
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.granite_golem_hp_aura)]
    public class GraniteAura : PassiveAbility
    {
        public GraniteAura(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}