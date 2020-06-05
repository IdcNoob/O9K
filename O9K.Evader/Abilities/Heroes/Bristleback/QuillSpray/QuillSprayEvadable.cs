namespace O9K.Evader.Abilities.Heroes.Bristleback.QuillSpray
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class QuillSprayEvadable : AreaOfEffectEvadable, IParticle, IModifierCounter
    {
        public QuillSprayEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);

            this.ModifierCounters.UnionWith(Abilities.SimplePhysShield);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierStackAllyObstacle(this, modifier, modifierOwner, 4);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddParticle(ParticleEffect particle, string name)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);

            var obstacle = new QuillSprayObstacle(this, particle.GetControlPoint(0), 125)
            {
                EndCastTime = time,
                EndObstacleTime = time + (this.ActiveAbility.Radius / this.ActiveAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}