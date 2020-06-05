namespace O9K.Evader.Abilities.Heroes.ShadowFiend.RequiemOfSouls
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.AreaOfEffect;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class RequiemOfSoulsEvadable : AreaOfEffectEvadable, IModifierCounter
    {
        public RequiemOfSoulsEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.Invulnerability);
            this.Disables.UnionWith(Abilities.StrongDisable);

            this.Counters.Add(Abilities.Swiftslash);
            this.Counters.Add(Abilities.BallLightning);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.UnionWith(Abilities.MagicImmunity);
            this.Counters.UnionWith(Abilities.StrongMagicShield);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongMagicShield);
        }

        public override bool CanBeDodged { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        protected override void AddObstacle()
        {
            var obstacle = new AreaOfEffectSpeedObstacle(this, this.Owner.Position, 350, 100)
            {
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.ActiveAbility.Radius / this.ActiveAbility.Speed)
            };

            this.Pathfinder.AddObstacle(obstacle);
        }
    }
}