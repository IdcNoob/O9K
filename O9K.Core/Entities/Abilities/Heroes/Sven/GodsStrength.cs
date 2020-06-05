namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.sven_gods_strength)]
    public class GodsStrength : ActiveAbility, IBuff
    {
        public GodsStrength(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_sven_gods_strength";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}