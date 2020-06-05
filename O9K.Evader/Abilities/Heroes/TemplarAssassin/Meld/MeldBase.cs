namespace O9K.Evader.Abilities.Heroes.TemplarAssassin.Meld
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.templar_assassin_meld)]
    internal class MeldBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public MeldBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MeldEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new MeldUsable(this.Ability, this.ActionManager, this.Menu);
        }
    }
}