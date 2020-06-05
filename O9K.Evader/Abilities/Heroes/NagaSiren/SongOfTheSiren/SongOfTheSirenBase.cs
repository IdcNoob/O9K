namespace O9K.Evader.Abilities.Heroes.NagaSiren.SongOfTheSiren
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.naga_siren_song_of_the_siren)]
    internal class SongOfTheSirenBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SongOfTheSirenBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SongOfTheSirenEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}