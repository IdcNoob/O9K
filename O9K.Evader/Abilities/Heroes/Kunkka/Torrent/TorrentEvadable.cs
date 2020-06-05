namespace O9K.Evader.Abilities.Heroes.Kunkka.Torrent
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

    internal sealed class TorrentEvadable : AreaOfEffectEvadable, IModifierCounter, IModifierObstacle
    {
        public TorrentEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.Spoink);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.Bulwark);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
        }

        public bool AllyModifierObstacle { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddModifierObstacle(Modifier modifier, Unit sender)
        {
            var time = Game.RawGameTime - modifier.ElapsedTime;

            var obstacle = new AreaOfEffectObstacle(this, sender.Position)
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.ActivationDelay,
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