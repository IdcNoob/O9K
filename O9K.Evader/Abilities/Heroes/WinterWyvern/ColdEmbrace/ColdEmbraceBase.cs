namespace O9K.Evader.Abilities.Heroes.WinterWyvern.ColdEmbrace
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.winter_wyvern_cold_embrace)]
    internal class ColdEmbraceBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public ColdEmbraceBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}