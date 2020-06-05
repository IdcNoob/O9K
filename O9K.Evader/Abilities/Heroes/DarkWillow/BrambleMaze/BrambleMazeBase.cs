namespace O9K.Evader.Abilities.Heroes.DarkWillow.BrambleMaze
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dark_willow_bramble_maze)]
    internal class BrambleMazeBase : EvaderBaseAbility, IEvadable
    {
        public BrambleMazeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BrambleMazeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}