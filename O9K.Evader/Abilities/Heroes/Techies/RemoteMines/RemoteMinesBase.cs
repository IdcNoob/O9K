namespace O9K.Evader.Abilities.Heroes.Techies.RemoteMines
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.techies_remote_mines)]
    internal class RemoteMinesBase : EvaderBaseAbility, IEvadable
    {
        public RemoteMinesBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RemoteMinesEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}