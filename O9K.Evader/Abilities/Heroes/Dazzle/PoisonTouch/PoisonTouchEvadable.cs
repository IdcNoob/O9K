namespace O9K.Evader.Abilities.Heroes.Dazzle.PoisonTouch
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Evadable;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.Projectile;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class PoisonTouchEvadable : TargetableProjectileEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> targetCounters = new HashSet<AbilityId>();

        public PoisonTouchEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.Meld);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.UnionWith(Abilities.Shield);

            this.Counters.Remove(Abilities.Bristleback);

            this.targetCounters.Add(Abilities.Counterspell);
            this.targetCounters.Add(Abilities.BallLightning);

            this.ModifierCounters.UnionWith(Abilities.AllyPurge);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        protected override IEnumerable<AbilityId> AllCounters
        {
            get
            {
                return base.AllCounters.Concat(this.targetCounters);
            }
        }

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override void AddProjectile(TrackingProjectile projectile, Unit9 target)
        {
            if (this.TargetableObstacle?.Projectile != null && this.EndCastTime + 0.5f > Game.RawGameTime)
            {
                this.TargetableObstacle.AddProjectile(projectile, target);
            }
            else
            {
                var projectileObstacle = new ProjectileObstacle(this, projectile, target);
                this.Pathfinder.AddObstacle(projectileObstacle);
            }
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            if (usableAbility?.Ability.Id == AbilityId.antimage_counterspell)
            {
                return obstacle.GetDisableTime(usableAbility.Owner) < 0.1f;
            }

            return base.IgnoreRemainingTime(obstacle, usableAbility);
        }

        protected override void AddObstacle()
        {
            this.TargetableObstacle = new TargetableProjectileObstacle(this)
            {
                Counters = this.targetCounters.ToArray()
            };

            this.Pathfinder.AddObstacle(this.TargetableObstacle);
        }
    }
}