namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.ChakraMagic
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.keeper_of_the_light_chakra_magic)]
    internal class ChakraMagicBase : EvaderBaseAbility, IEvadable
    {
        public ChakraMagicBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChakraMagicEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}