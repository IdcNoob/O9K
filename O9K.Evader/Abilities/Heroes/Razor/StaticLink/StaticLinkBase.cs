namespace O9K.Evader.Abilities.Heroes.Razor.StaticLink
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.razor_static_link)]
    internal class StaticLinkBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public StaticLinkBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StaticLinkEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}