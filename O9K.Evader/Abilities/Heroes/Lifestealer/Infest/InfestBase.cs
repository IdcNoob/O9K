namespace O9K.Evader.Abilities.Heroes.Lifestealer.Infest
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.life_stealer_infest)]
    internal class InfestBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public InfestBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new InfestUsable(this.Ability, this.Menu);
        }
    }
}