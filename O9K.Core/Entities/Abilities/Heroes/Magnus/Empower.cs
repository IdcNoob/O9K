namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.magnataur_empower)]
    public class Empower : RangedAbility, IBuff
    {
        public Empower(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_magnataur_empower";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}