namespace O9K.Evader.Abilities.Heroes.StormSpirit.StaticRemnant
{
    using System.Collections.Generic;
    using System.Linq;

    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Entity;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;

    internal sealed class StaticRemnantEvadable : AreaOfEffectEvadable, IUnit
    {
        private readonly HashSet<AbilityId> singleCounters = new HashSet<AbilityId>();

        public StaticRemnantEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.Shield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.Add(Abilities.BladeMail);

            this.singleCounters.Add(Abilities.Refraction);
            this.singleCounters.Add(Abilities.SpikedCarapace);
            this.singleCounters.Add(Abilities.AttributeShift);
            this.singleCounters.UnionWith(Abilities.MagicShield);
            this.singleCounters.UnionWith(Abilities.Heal);
            this.singleCounters.Add(Abilities.Armlet);
            this.singleCounters.UnionWith(Abilities.Suicide);
            this.singleCounters.Add(Abilities.BladeMail);
        }

        public override bool CanBeDodged { get; } = false;

        public void AddUnit(Unit unit)
        {
            var time = Game.RawGameTime - (Game.Ping / 2000);

            var obstacle = new AreaOfEffectObstacle(this, unit.Position)
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.ActivationDelay,
            };

            if (EntityManager9.Units.Count(
                    x => x.IsUnit && !x.IsAlly(this.Owner) && x.Distance(this.Owner) < this.Ability.Radius && x.IsAlive
                         && !x.IsInvulnerable) <= 1)
            {
                obstacle.Counters = this.singleCounters.ToArray();
            }

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