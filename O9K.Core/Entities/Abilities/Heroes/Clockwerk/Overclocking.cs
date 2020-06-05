namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.rattletrap_overclocking)]
    public class Overclocking : ActiveAbility, IBuff
    {
        public Overclocking(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_rattletrap_overclocking";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}