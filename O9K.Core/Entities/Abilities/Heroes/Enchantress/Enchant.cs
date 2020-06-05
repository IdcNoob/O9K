namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.enchantress_enchant)]
    public class Enchant : RangedAbility, IDebuff
    {
        public Enchant(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string DebuffModifierName { get; } = "modifier_enchantress_enchant_slow";
    }
}