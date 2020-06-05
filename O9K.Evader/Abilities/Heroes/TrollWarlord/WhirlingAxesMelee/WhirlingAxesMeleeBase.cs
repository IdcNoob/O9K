namespace O9K.Evader.Abilities.Heroes.TrollWarlord.WhirlingAxesMelee
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.troll_warlord_whirling_axes_melee)]
    internal class WhirlingAxesMeleeBase : EvaderBaseAbility, /*IEvadable, */IUsable<CounterAbility>
    {
        public WhirlingAxesMeleeBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WhirlingAxesMeleeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            //todo dont use vs true strike
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}