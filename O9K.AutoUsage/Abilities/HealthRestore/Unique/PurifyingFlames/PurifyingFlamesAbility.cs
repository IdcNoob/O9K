namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.PurifyingFlames
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.oracle_purifying_flames)]
    internal class PurifyingFlamesAbility : HealthRestoreAbility
    {
        private readonly PurifyingFlamesSettings settings;

        public PurifyingFlamesAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new PurifyingFlamesSettings(settings.Menu, healthRestore);
        }

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

                if (this.settings.NoDamageOnly && !ally.HasModifier("modifier_oracle_fates_edict", "modifier_oracle_false_promise_timer"))
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

                if (!this.Ability.CanHit(ally))
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

                return this.Ability.UseAbility(ally);
            }

            return false;
        }
    }
}