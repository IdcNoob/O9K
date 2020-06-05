namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_imp_claw)]
    public class ImpClaw : PassiveAbility
    {
        public ImpClaw(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}