namespace O9K.Evader.Abilities.Heroes.SandKing.Burrowstrike
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;
    using Pathfinder.Obstacles.Abilities.Proactive;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class BurrowstrikeEvadable : LinearProjectileEvadable, IParticle, IModifierCounter, IProactiveCounter
    {
        public BurrowstrikeEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.UnionWith(Abilities.VsDisableProjectile);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);

            this.ProactiveBlinks.UnionWith(Abilities.ProactiveBlink);

            this.ProactiveCounters.UnionWith(Abilities.ProactiveHexCounter);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);
            var startPosition = particle.GetControlPoint(0);
            var endPosition = particle.GetControlPoint(1);

            var obstacle = new LinearProjectileObstacle(this, startPosition, endPosition)
            {
                EndCastTime = time,
                EndObstacleTime = time + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddProactiveObstacle()
        {
            var obstacle = new ProactiveAbilityObstacle(this);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}