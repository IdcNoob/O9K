namespace O9K.Evader.Abilities.Heroes.Grimstroke.InkSwell
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.grimstroke_spirit_walk)]
    internal class InkSwellBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public InkSwellBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new InkSwellEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}