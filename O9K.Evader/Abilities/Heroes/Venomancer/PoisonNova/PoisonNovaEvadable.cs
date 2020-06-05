namespace O9K.Evader.Abilities.Heroes.Venomancer.PoisonNova
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class PoisonNovaEvadable : AreaOfEffectEvadable, IModifierCounter, IParticle
    {
        public PoisonNovaEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Snowball);
            this.Counters.Add(Abilities.Doppelganger);
            this.Counters.Add(Abilities.PhaseShift);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);

            this.Counters.Remove(Abilities.FatesEdict);
            this.Counters.Remove(Abilities.Nightmare);

            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
            this.ModifierCounters.Add(Abilities.HeavenlyGrace);
            this.ModifierCounters.Add(Abilities.PressTheAttack);

            this.ModifierCounters.Remove(Abilities.Nightmare);
            this.ModifierCounters.Remove(Abilities.FatesEdict);
            this.ModifierCounters.Remove(Abilities.Bristleback);
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

            var obstacle = new AreaOfEffectSpeedObstacle(this, particle.GetControlPoint(0), 300)
            {
                EndCastTime = time,
                EndObstacleTime = time + (this.ActiveAbility.Radius / this.ActiveAbility.Speed)
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