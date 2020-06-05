namespace O9K.Evader.Abilities.Heroes.Invoker.ChaosMeteor
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.LinearProjectile;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class ChaosMeteorEvadable : LinearProjectileEvadable, IModifierCounter, IParticle
    {
        public ChaosMeteorEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.EulsScepterOfDivinity);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.MagicShield);
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
            var casterPosition = particle.GetControlPoint(0);
            var landPosition = particle.GetControlPoint(1);

            //todo improve evade timings
            var obstacle = new LinearProjectileObstacle(
                this,
                landPosition.Extend2D(casterPosition, this.RangedAbility.Radius / 2),
                casterPosition.Extend2D(landPosition, 2000))
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.ActivationDelay + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

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