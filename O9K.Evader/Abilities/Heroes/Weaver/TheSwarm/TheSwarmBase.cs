namespace O9K.Evader.Abilities.Heroes.Weaver.TheSwarm
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.weaver_the_swarm)]
    internal class TheSwarmBase : EvaderBaseAbility, IEvadable
    {
        public TheSwarmBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TheSwarmEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}