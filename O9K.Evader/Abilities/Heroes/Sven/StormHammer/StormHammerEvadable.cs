namespace O9K.Evader.Abilities.Heroes.Sven.StormHammer
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles.Abilities.Projectile;
    using Pathfinder.Obstacles.Modifiers;

    internal sealed class StormHammerEvadable : TargetableProjectileEvadable, IModifierCounter
    {
        private StormHammerAoeObstacle stormHammerAoeObstacle;

        private TargetableProjectileObstacle targetableObstacle;

        public StormHammerEvadable(Ability9 ability, IPathfinder pathfinder, IMainMenu menu)
            : base(ability, pathfinder, menu)
        {
            //todo merge 2 obstacles into 1?
            this.RangedAbility = (RangedAbility)ability;

            this.Blinks.UnionWith(Abilities.Blink);

            this.Disables.UnionWith(Abilities.Disable);
            this.Disables.UnionWith(Abilities.PhysDisable);

            this.Counters.UnionWith(Abilities.VsDisableProjectile);
            this.Counters.Add(Abilities.AttributeShift);
            this.Counters.UnionWith(Abilities.StrongShield);
            this.Counters.UnionWith(Abilities.Invulnerability);
            this.Counters.Add(Abilities.LotusOrb);
            this.Counters.UnionWith(Abilities.StrongPhysShield);
            this.Counters.Add(Abilities.Bulwark);
            this.Counters.UnionWith(Abilities.Heal);
            this.Counters.Add(Abilities.Armlet);
            this.Counters.UnionWith(Abilities.Suicide);
            this.Counters.UnionWith(Abilities.Invisibility);
            this.Counters.UnionWith(Abilities.SlowHeal);
            this.Counters.Add(Abilities.BladeMail);
            this.Counters.Add(Abilities.HurricanePike);

            this.ModifierCounters.UnionWith(Abilities.AllyStrongPurge);
            this.ModifierCounters.UnionWith(Abilities.Invulnerability);
            this.ModifierCounters.UnionWith(Abilities.StrongPhysShield);
        }

        public override bool IsDisjointable { get; } = false;

        public bool ModifierAllyCounter { get; } = true;

        public bool ModifierEnemyCounter { get; } = false;

        public RangedAbility RangedAbility { get; }

        public void AddModifier(Modifier modifier, Unit9 modifierOwner)
        {
            var obstacle = new ModifierAllyObstacle(this, modifier, modifierOwner);
            this.Pathfinder.AddObstacle(obstacle);
        }

        public override void AddProjectile(TrackingProjectile projectile, Unit9 target)
        {
            if (this.targetableObstacle != null && this.EndCastTime + 0.5f > Game.RawGameTime)
            {
                this.targetableObstacle.AddProjectile(projectile, target);
                this.stormHammerAoeObstacle.AddProjectile(projectile, target);
            }
            else
            {
                var projectileObstacle = new ProjectileObstacle(this, projectile, target);
                this.Pathfinder.AddObstacle(projectileObstacle);

                var aoeObstacle = new StormHammerAoeObstacle(this, projectile, target);
                this.Pathfinder.AddObstacle(aoeObstacle);
            }
        }

        public override void PhaseCancel()
        {
            base.PhaseCancel();

            this.Pathfinder.CancelObstacle(this.Ability.Handle);
        }

        protected override void AddObstacle()
        {
            this.targetableObstacle = new TargetableProjectileObstacle(this);
            this.Pathfinder.AddObstacle(this.targetableObstacle);

            this.stormHammerAoeObstacle = new StormHammerAoeObstacle(this)
            {
                Id = this.targetableObstacle.Id,
                EndCastTime = this.EndCastTime,
                EndObstacleTime = this.EndCastTime + (this.RangedAbility.Range / this.RangedAbility.Speed)
            };

            this.Pathfinder.AddObstacle(this.stormHammerAoeObstacle);
        }
    }
}