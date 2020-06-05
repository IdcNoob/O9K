namespace O9K.Evader.Abilities.Heroes.Lich.FrostBlast
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearAreaOfEffect;
    using Pathfinder.Obstacles.Abilities.Targetable;

    internal sealed class FrostBlastEvadable : LinearAreaOfEffectEvadable
    {
        private readonly HashSet<AbilityId> targetCounters = new HashSet<AbilityId>();

        public FrostBlastEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.targetCounters.Add(Abilities.Counterspell);
        }

        protected override IEnumerable<AbilityId> AllCounters
        {
            get
            {
                return base.AllCounters.Concat(this.targetCounters);
            }
        }

        public override void PhaseCancel()
        {
            base.PhaseCancel();

            this.Pathfinder.CancelObstacle(this.Ability.Handle);
        }

        protected override void AddObstacle()
        {
            var obstacle = new LinearAreaOfEffectObstacle(this, this.Owner.Position)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay,
            };

            this.Pathfinder.AddObstacle(obstacle);

            var targetableObstacle = new TargetableObstacle(this)
            {
                Id = obstacle.Id,
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime,
                Counters = this.targetCounters.ToArray()
            };

            this.Pathfinder.AddObstacle(targetableObstacle);
        }
    }
}