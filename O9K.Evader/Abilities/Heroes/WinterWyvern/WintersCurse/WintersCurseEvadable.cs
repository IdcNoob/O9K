namespace O9K.Evader.Abilities.Heroes.WinterWyvern.WintersCurse
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
    using Pathfinder.Obstacles.Abilities.LinearAreaOfEffect;
    using Pathfinder.Obstacles.Abilities.Targetable;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class WintersCurseEvadable : LinearAreaOfEffectEvadable, IModifierCounter
    {
        private readonly HashSet<AbilityId> targetCounters = new HashSet<AbilityId>();

        public WintersCurseEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.StrongDisable);
            this.Disables.Remove(Abilities.WilloWisp);

            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.Snowball);
            this.Counters.Add(Abilities.EulsScepterOfDivinity);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.MagicImmunity);

            this.targetCounters.Add(Abilities.Counterspell);
            this.targetCounters.Add(Abilities.LinkensSphere);
            this.targetCounters.Add(Abilities.LotusOrb);
            this.targetCounters.UnionWith(Abilities.StrongPhysShield);
            this.targetCounters.Add(Abilities.AttributeShift);
            this.targetCounters.Add(Abilities.Enrage);
            this.targetCounters.Add(Abilities.Bulldoze);
            this.targetCounters.UnionWith(Abilities.SlowHeal);

            this.targetCounters.Remove(Abilities.StaticLink);

            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
            this.ModifierCounters.UnionWith(Abilities.SlowHeal);

            this.ModifierCounters.Remove(Abilities.StaticLink);
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
            var modifierObstacle = new ModifierAllyObstacle(this, modifier, modifierOwner)
            {
                IgnoreModifierRemainingTime = true
            };

            this.Pathfinder.AddObstacle(modifierObstacle);

            var aoeObstacle = new WintersCurseObstacle(this, modifierOwner.Position, modifier);
            this.Pathfinder.AddObstacle(aoeObstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            return obstacle.IsModifierObstacle || obstacle is WintersCurseObstacle;
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
                //Id = obstacle.Id,
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay,
                Counters = this.targetCounters.ToArray()
            };

            this.Pathfinder.AddObstacle(targetableObstacle);
        }
    }
}