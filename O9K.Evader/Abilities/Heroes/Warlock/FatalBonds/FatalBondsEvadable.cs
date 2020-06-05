namespace O9K.Evader.Abilities.Heroes.Warlock.FatalBonds
{
    using System.Collections.Generic;
    using System.Linq;

    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearAreaOfEffect;
    using Pathfinder.Obstacles.Abilities.Targetable;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class FatalBondsEvadable : LinearAreaOfEffectEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> targetCounters = new HashSet<AbilityId>();

        public FatalBondsEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.SleightOfFist);

            this.targetCounters.Add(Abilities.Counterspell);
            this.targetCounters.Add(Abilities.LinkensSphere);
            this.targetCounters.Add(Abilities.LotusOrb);

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