namespace O9K.Core.Entities.Abilities.Heroes.Omniknight
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.omniknight_degen_aura)]
    public class DegenAura : PassiveAbility
    {
        public DegenAura(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}