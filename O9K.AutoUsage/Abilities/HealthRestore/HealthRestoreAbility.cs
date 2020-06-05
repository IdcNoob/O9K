namespace O9K.AutoUsage.Abilities.HealthRestore
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Ensage.SDK.Extensions;

    using Settings;

    internal class HealthRestoreAbility : UsableAbility
    {
        private readonly HealthRestoreSettings settings;

        public HealthRestoreAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.HealthRestore = healthRestore;
            this.settings = new HealthRestoreSettings(settings.Menu, healthRestore);
        }

        public HealthRestoreAbility(IHealthRestore healthRestore)
            : base(healthRestore)
        {
            this.HealthRestore = healthRestore;
        }

        protected IHealthRestore HealthRestore { get; }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var lowMp = this.Owner.ManaPercentage < this.settings.MpThreshold;

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).OrderBy(x => x.Health).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).ToList();

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && !this.settings.SelfOnly)
                {
                    continue;
                }

                if (!ally.CanBeHealed)
                {
                    continue;
                }

                var healthPercentage = ally.HealthPercentage;

                if (healthPercentage > this.settings.HpThreshold || (lowMp && healthPercentage > this.settings.CriticalHpThreshold))
                {
                    continue;
                }

                var selfTarget = ally.Equals(this.Owner);

                if (selfTarget && !this.HealthRestore.RestoresOwner)
                {
                    continue;
                }

                if (!selfTarget && (!this.HealthRestore.RestoresAlly || this.settings.SelfOnly))
                {
                    continue;
                }

                if (!this.Ability.CanHit(ally, allies, this.settings.AlliesCount))
                {
                    continue;
                }

                if (ally.BaseUnit.HasAnyModifiers(this.HealthRestore.RestoreModifierName))
                {
                    continue;
                }

                if (enemies.Count(x => x.Distance(ally) < this.settings.Distance) < this.settings.EnemiesCount)
                {
                    continue;
                }

                return this.Ability.UseAbility(ally, allies, HitChance.Medium, this.settings.AlliesCount);
            }

            return false;
        }
    }
}