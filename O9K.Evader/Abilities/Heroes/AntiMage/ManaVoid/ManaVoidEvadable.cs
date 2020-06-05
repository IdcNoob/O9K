namespace O9K.Evader.Abilities.Heroes.AntiMage.ManaVoid
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearAreaOfEffect;
    using Pathfinder.Obstacles.Abilities.Targetable;

    internal sealed class ManaVoidEvadable : LinearAreaOfEffectEvadable
    {
        private readonly HashSet<AbilityId> targetCounters = new HashSet<AbilityId>();

        public ManaVoidEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.UnionWith(Abilities.MagicImmunity);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Enrage);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.Counters.Remove(Abilities.DarkPact);

            this.targetCounters.Add(Abilities.Counterspell);
            this.targetCounters.Add(Abilities.LinkensSphere);
            this.targetCounters.Add(Abilities.LotusOrb);
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