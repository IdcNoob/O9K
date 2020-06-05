namespace O9K.Evader.Abilities.Heroes.Windranger.Windrun
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.windrunner_windrun)]
    internal class WindrunBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DodgeAbility>
    {
        public WindrunBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WindrunEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }

        DodgeAbility IUsable<DodgeAbility>.GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}