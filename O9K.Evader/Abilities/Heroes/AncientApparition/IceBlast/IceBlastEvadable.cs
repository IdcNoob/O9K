namespace O9K.Evader.Abilities.Heroes.AncientApparition.IceBlast
{
    using System;

    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class IceBlastEvadable : AreaOfEffectEvadable, IModifierCounter, IUnit, IParticle
    {
        private readonly SpecialData growRadius;

        private readonly SpecialData maxRadius;

        private readonly SpecialData minRadius;

        private float unitAddTime;

        public IceBlastEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo add line obstacle
            //todo modifier invul when low hp 

            this.minRadius = new SpecialData(this.Ability.BaseAbility, "radius_min");
            this.maxRadius = new SpecialData(this.Ability.BaseAbility, "radius_max");
            this.growRadius = new SpecialData(this.Ability.BaseAbility, "radius_grow");

            this.Blinks.UnionWith(Abilities.Blink);

            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.Snowball);
            this.Counters.Add(Abilities.TricksOfTheTrade);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Suicide);

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
            if (this.unitAddTime <= 0)
            {
                return;
            }

            var time = Game.RawGameTime - (Game.Ping / 2000);
            var startPosition = particle.GetControlPoint(0);
            var endPosition = particle.GetControlPoint(1);
            var direction = startPosition + endPosition;
            var flyingTime = time - this.unitAddTime;
            var position = startPosition.Extend2D(direction, flyingTime * this.ActiveAbility.Speed);
            var radius = Math.Min(
                this.maxRadius.GetValue(1),
                Math.Max((flyingTime * this.growRadius.GetValue(1)) + this.minRadius.GetValue(1), this.minRadius.GetValue(1)));

            var obstacle = new IceBlastObstacle(this, position, radius)
            {
                EndCastTime = time,
                EndObstacleTime = time + particle.GetControlPoint(5).X
            };

            this.Pathfinder.AddObstacle(obstacle);
            this.unitAddTime = 0;
        }

        public void AddUnit(Unit unit)
        {
            this.unitAddTime = Game.RawGameTime - (Game.Ping / 2000);
        }

        protected override void AddObstacle()
        {
        }
    }
}