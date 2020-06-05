namespace O9K.Evader.Abilities.Heroes.Morphling.AdaptiveStrikeAgility
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.morphling_adaptive_strike_agi)]
    internal class AdaptiveStrikeAgilityBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public AdaptiveStrikeAgilityBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AdaptiveStrikeAgilityEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}