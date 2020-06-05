namespace O9K.Evader.Abilities.Heroes.Lich.ChainFrost
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lich_chain_frost)]
    internal class ChainFrostBase : EvaderBaseAbility, IEvadable
    {
        public ChainFrostBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ChainFrostEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}