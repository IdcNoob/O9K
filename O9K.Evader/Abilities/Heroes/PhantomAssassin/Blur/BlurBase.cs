namespace O9K.Evader.Abilities.Heroes.PhantomAssassin.Blur
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phantom_assassin_blur)]
    internal class BlurBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public BlurBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new BlurUsable(this.Ability, this.Menu);
        }
    }
}