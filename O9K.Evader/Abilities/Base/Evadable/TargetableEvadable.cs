namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.Targetable;

    internal abstract class TargetableEvadable : EvadableAbility
    {
        protected TargetableEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        public override bool CanBeDodged { get; } = false;

        protected override void AddObstacle()
        {
            var obstacle = new TargetableObstacle(this)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}