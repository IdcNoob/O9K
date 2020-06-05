namespace O9K.Evader.Abilities.Heroes.Mars.Bulwark
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mars_bulwark)]
    internal class BulwarkBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public BulwarkBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new BulwarkUsable(this.Ability, this.Menu);
        }
    }
}