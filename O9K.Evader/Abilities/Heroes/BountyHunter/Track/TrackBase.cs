namespace O9K.Evader.Abilities.Heroes.BountyHunter.Track
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bounty_hunter_track)]
    internal class TrackBase : EvaderBaseAbility, IEvadable
    {
        public TrackBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TrackEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}