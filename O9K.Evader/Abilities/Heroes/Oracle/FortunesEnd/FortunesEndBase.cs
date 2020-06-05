namespace O9K.Evader.Abilities.Heroes.Oracle.FortunesEnd
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.oracle_fortunes_end)]
    internal class FortunesEndBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>, IUsable<CounterAbility>
    {
        public FortunesEndBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FortunesEndEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new FortunesEndUsableDisable(this.Ability, this.ActionManager, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new FortunesEndUsableCounter(this.Ability, this.ActionManager, this.Menu);
        }
    }
}