namespace O9K.Evader.Abilities.Heroes.Earthshaker.EchoSlam
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;
    using Base.Usable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;
    using Pathfinder.Obstacles.Abilities.Proactive;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class EchoSlamEvadable : AreaOfEffectEvadable, IModifierCounter, IProactiveCounter
    {
        public EchoSlamEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.ProactiveBlinks.UnionWith(Abilities.ProactiveBlink);

            this.ProactiveDisables.UnionWith(Abilities.ProactiveItemDisable);

            this.ProactiveCounters.UnionWith(Abilities.ProactiveHexCounter);
            this.ProactiveCounters.Add(Abilities.Doppelganger);

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

        public void AddProactiveObstacle()
        {
            var obstacle = new ProactiveAreaOfEffectObstacle(this);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override bool IgnoreRemainingTime(IObstacle obstacle, UsableAbility usableAbility)
        {
            // for proactive obstacle
            return true;
        }

        public override void PhaseCancel()
        {
        }

        public override void PhaseStart()
        {
        }
    }
}