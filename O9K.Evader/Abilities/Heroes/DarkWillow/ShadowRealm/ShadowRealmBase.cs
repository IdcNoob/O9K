namespace O9K.Evader.Abilities.Heroes.DarkWillow.ShadowRealm
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_shadow_realm)]
    internal class ShadowRealmBase : EvaderBaseAbility, /*IEvadable,*/ IUsable<CounterAbility>
    {
        public ShadowRealmBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            //todo fix
            return new ShadowRealmEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new ShadowRealmUsable(this.Ability, this.Menu);
        }
    }
}