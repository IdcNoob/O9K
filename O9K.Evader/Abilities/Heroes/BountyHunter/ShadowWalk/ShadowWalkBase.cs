namespace O9K.Evader.Abilities.Heroes.BountyHunter.ShadowWalk
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bounty_hunter_wind_walk)]
    internal class ShadowWalkBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public ShadowWalkBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }
    }
}