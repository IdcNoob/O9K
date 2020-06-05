namespace O9K.Evader.Abilities.Heroes.PhantomLancer.Doppelganger
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phantom_lancer_doppelwalk)]
    internal class DoppelgangerBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public DoppelgangerBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new DoppelgangerUsable(this.Ability, this.Menu);
        }
    }
}