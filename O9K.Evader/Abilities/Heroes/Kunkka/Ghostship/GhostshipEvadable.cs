namespace O9K.Evader.Abilities.Heroes.Kunkka.Ghostship
{
    using Base;
    using Base.Evadable;
    using Base.Evadable.Components;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Kunkka;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Modifiers;

    internal sealed class GhostshipEvadable : LinearProjectileEvadable, IUnit, IModifierCounter
    {
        private readonly Sleeper sleeper = new Sleeper();

        public GhostshipEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo fix move with aghs?
            //todo fix 3x talent
            this.Ghostship = (Ghostship)ability;

            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);

            this.Counters.Add(Abilities.SleightOfFist);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.Mischief);
            this.Counters.Add(Abilities.MantaStyle);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.HurricanePike);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.Bulwark);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.Add(Abilities.PressTheAttack);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public Ghostship Ghostship { get; }

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public void AddUnit(Unit unit)
        {
            if (this.sleeper)
            {
                // 2 units summoned with aghs
                return;
            }

            this.sleeper.Sleep(0.1f);

            var time = Game.RawGameTime - (Game.Ping / 2000) - 0.1f;

            var obstacle = new GhostshipObstacle(this, unit)
            {
                EndCastTime = time,
                EndObstacleTime = time + this.Ability.ActivationDelay
            };

            this.Pathfinder.AddObstacle(obstacle);
        }

        protected override void AddObstacle()
        {
        }
    }
}