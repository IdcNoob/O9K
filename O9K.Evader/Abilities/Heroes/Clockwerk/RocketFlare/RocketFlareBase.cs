namespace O9K.Evader.Abilities.Heroes.Clockwerk.RocketFlare
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.rattletrap_rocket_flare)]
    internal class RocketFlareBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public RocketFlareBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new RocketFlareEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}