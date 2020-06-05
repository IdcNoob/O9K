namespace O9K.Evader.Abilities.Heroes.Terrorblade.Sunder
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.terrorblade_sunder)]
    internal class SunderBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SunderBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SunderEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new SunderUsable(this.Ability, this.Menu);
        }
    }
}