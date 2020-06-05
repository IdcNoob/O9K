namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    internal abstract class AreaOfEffectEvadable : EvadableAbility
    {
        protected AreaOfEffectEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
        }

        protected override void AddObstacle()
        {
            var obstacle = new AreaOfEffectObstacle(this, this.Owner.Position)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay,
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}