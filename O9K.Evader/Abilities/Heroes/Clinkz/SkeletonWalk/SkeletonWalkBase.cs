namespace O9K.Evader.Abilities.Heroes.Clinkz.SkeletonWalk
{
    using Base;
    using Base.Usable.CounterAbility;
    using Base.Usable.DodgeAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.clinkz_wind_walk)]
    internal class SkeletonWalkBase : EvaderBaseAbility, IUsable<CounterAbility>, IUsable<DodgeAbility>
    {
        public SkeletonWalkBase(Ability9 ability)
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