namespace O9K.Evader.Abilities.Heroes.TreantProtector.NaturesGrasp
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.treant_natures_grasp)]
    internal class NaturesGraspBase : EvaderBaseAbility, IUsable<CounterEnemyAbility>
    {
        public NaturesGraspBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}