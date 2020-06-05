namespace O9K.Evader.Abilities.Heroes.Necrophos.GhostShroud
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.necrolyte_sadist)]
    internal class GhostShroudBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public GhostShroudBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GhostShroudEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}