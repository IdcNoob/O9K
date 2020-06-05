namespace O9K.Core.Entities.Abilities.Units.Necronomicon.Archer
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.necronomicon_archer_aoe)]
    public class ArcherAura : PassiveAbility
    {
        public ArcherAura(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}