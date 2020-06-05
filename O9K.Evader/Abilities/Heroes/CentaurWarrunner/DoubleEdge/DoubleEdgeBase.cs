namespace O9K.Evader.Abilities.Heroes.CentaurWarrunner.DoubleEdge
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.centaur_double_edge)]
    internal class DoubleEdgeBase : EvaderBaseAbility, IEvadable
    {
        public DoubleEdgeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DoubleEdgeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}