namespace O9K.Evader.Abilities.Heroes.Invoker.GhostWalk
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.invoker_ghost_walk)]
    internal class GhostWalkBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public GhostWalkBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }
    }
}