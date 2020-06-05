namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.lich_frost_shield)]
    public class FrostShield : RangedAbility, IBuff
    {
        public FrostShield(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string BuffModifierName { get; } = "modifier_lich_frost_shield";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;
    }
}