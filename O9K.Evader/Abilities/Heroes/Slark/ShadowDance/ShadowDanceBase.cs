namespace O9K.Evader.Abilities.Heroes.Slark.ShadowDance
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.slark_shadow_dance)]
    internal class ShadowDanceBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public ShadowDanceBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}