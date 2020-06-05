namespace O9K.Evader.Abilities.Heroes.Juggernaut.BladeFury
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.juggernaut_blade_fury)]
    internal class BladeFuryBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BladeFuryBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BladeFuryEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}