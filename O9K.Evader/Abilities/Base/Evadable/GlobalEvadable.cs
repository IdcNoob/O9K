namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.Global;

    internal abstract class GlobalEvadable : EvadableAbility
    {
        protected GlobalEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override bool CanBeDodged { get; } = false;

        protected override void AddObstacle()
        {
            var obstacle = new GlobalObstacle(this)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}