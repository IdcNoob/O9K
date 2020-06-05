namespace O9K.Evader.Abilities.Heroes.Weaver.Shukuchi
{
    using Base;
    using Base.Usable.CounterAbility;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.weaver_shukuchi)]
    internal class ShukuchiBase : EvaderBaseAbility, IUsable<CounterAbility>, IUsable<DodgeAbility>
    {
        public ShukuchiBase(Ability9 ability)
            : base(ability)
        {
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }

        DodgeAbility IUsable<DodgeAbility>.GetUsableAbility()
        {
            return new DodgeAbility(this.Ability, this.Menu);
        }
    }
}