namespace O9K.Evader.Abilities.Heroes.ShadowDemon.ShadowPoison
{
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;

    internal sealed class ShadowPoisonEvadable : LinearProjectileEvadable, IUnit
    {
        public ShadowPoisonEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.PhaseShift);
        }

        public void AddUnit(Unit unit)
        {
            if (this.Owner.IsVisible)
            {
                return;
            }

            var time = Game.RawGameTime - (Game.Ping / 2000);

            var obstacle = new LinearProjectileUnitObstacle(this, unit)
            {
                EndCastTime = time,
                EndObstacleTime = time + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}