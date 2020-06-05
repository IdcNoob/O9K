namespace O9K.Evader.Abilities.Heroes.ElderTitan.EchoStomp
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class EchoStompEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public EchoStompEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo add spirit
            //todo add ability obstacle cancel after chanel start

            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.MagicShield);
            this.Counters.UnionWith(Abilities.SlowHeal);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.Add(Abilities.PurifyingFlames);
        }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var modifierObstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(modifierObstacle);
        }

        protected override void AddObstacle()
        {
            var obstacle = new AreaOfEffectObstacle(this, this.Owner.Position)
            {
                EndCastTime = this.EndCastTime + this.Ability.ActivationDelay,
                EndObstacleTime = this.EndCastTime + this.Ability.ActivationDelay,
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}