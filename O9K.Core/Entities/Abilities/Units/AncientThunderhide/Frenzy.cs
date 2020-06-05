namespace O9K.Core.Entities.Abilities.Units.AncientThunderhide
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.big_thunder_lizard_frenzy)]
    public class Frenzy : RangedAbility, IBuff
    {
        public Frenzy(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_big_thunder_lizard_frenzy";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}