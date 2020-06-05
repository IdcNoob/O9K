namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;

    internal abstract class LinearProjectileEvadable : EvadableAbility
    {
        protected LinearProjectileEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.RangedAbility = (RangedAbility)ability;
        }

        public RangedAbility RangedAbility { get; }

        protected override void AddObstacle()
        {
            var obstacle = new LinearProjectileObstacle(this, this.Ability.Owner.Position)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}