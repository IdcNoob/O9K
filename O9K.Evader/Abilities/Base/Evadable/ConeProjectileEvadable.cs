namespace O9K.Evader.Abilities.Base.Evadable
{
    using Core.Entities.Abilities.Base;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.ConeProjectile;

    internal class ConeProjectileEvadable : EvadableAbility
    {
        public ConeProjectileEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ConeAbility = (ConeAbility)ability;
        }

        public ConeAbility ConeAbility { get; }

        protected override void AddObstacle()
        {
            var obstacle = new ConeProjectileObstacle(this, this.Owner.Position)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.ConeAbility.Range / this.ConeAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}