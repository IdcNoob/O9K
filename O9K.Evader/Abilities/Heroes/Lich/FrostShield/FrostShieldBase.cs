namespace O9K.Evader.Abilities.Heroes.Lich.FrostShield
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lich_frost_shield)]
    internal class FrostShieldBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public FrostShieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FrostShieldEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}