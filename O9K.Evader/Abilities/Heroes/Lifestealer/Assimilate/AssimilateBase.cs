namespace O9K.Evader.Abilities.Heroes.Lifestealer.Assimilate
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.life_stealer_assimilate)]
    internal class AssimilateBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public AssimilateBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}