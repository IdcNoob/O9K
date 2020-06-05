namespace O9K.Evader.Abilities.Heroes.Omniknight.HeavenlyGrace
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.omniknight_repel)]
    internal class HeavenlyGraceBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public HeavenlyGraceBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new HeavenlyGraceUsable(this.Ability, this.ActionManager, this.Menu);
        }
    }
}