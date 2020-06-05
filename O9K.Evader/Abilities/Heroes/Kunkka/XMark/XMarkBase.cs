namespace O9K.Evader.Abilities.Heroes.Kunkka.XMark
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.kunkka_x_marks_the_spot)]
    internal class XMarkBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public XMarkBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new XMarkEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new XMarkUsable(this.Ability, this.Menu);
        }
    }
}