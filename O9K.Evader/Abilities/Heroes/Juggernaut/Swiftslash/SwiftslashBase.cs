namespace O9K.Evader.Abilities.Heroes.Juggernaut.Swiftslash
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    using Omnislash;

    [AbilityId((AbilityId)419)] // swift slash
    internal class SwiftslashBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SwiftslashBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OmnislashEvadabe(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}