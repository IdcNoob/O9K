namespace O9K.Evader.Abilities.Heroes.Enchantress.Enchant
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.enchantress_enchant)]
    internal class EnchantBase : EvaderBaseAbility, IEvadable
    {
        public EnchantBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EnchantEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}