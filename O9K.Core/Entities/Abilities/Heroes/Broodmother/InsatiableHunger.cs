namespace O9K.Core.Entities.Abilities.Heroes.Broodmother
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.broodmother_insatiable_hunger)]
    public class InsatiableHunger : ActiveAbility, IBuff, IHasLifeSteal
    {
        public InsatiableHunger(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_broodmother_insatiable_hunger";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}