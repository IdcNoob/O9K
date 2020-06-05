namespace O9K.Evader.Abilities.Heroes.Timbersaw.TimberChain
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shredder_timber_chain)]
    internal class TimberChainBase : EvaderBaseAbility, IEvadable
    {
        public TimberChainBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TimberChainEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}