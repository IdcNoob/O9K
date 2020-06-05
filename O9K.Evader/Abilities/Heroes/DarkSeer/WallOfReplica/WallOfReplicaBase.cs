namespace O9K.Evader.Abilities.Heroes.DarkSeer.WallOfReplica
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_seer_wall_of_replica)]
    internal class WallOfReplicaBase : EvaderBaseAbility, IEvadable
    {
        public WallOfReplicaBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WallOfReplicaEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}