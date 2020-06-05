namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.obsidian_destroyer_equilibrium)]
    public class EssenceFlux : ActiveAbility, IBuff
    {
        public EssenceFlux(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_obsidian_destroyer_equilibrium_active";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}