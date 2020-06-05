namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.SoulRip
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.undying_soul_rip)]
    internal class SoulRipAbility : HealthRestoreAbility
    {
        private readonly SoulRipSettings settings;

        public SoulRipAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new SoulRipSettings(settings.Menu, healthRestore);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var lowMp = this.Owner.ManaPercentage < this.settings.MpThreshold;

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).OrderBy(x => x.Health).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).ToList();

            if (this.settings.UseOnTombstone)
            {
                allies.AddRange(
                    EntityManager9.Units.Where(
                        x => x.BaseUnit.NetworkName == "CDOTA_Unit_Undying_Tombstone" && x.IsAlive && x.IsAlly(this.Owner)));
            }

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && ally.IsHero && !this.settings.SelfOnly)
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

                if (this.Owner.Distance(ally) > this.Ability.CastRange)
                {
                    continue;
                }

                if (ally.IsHero && enemies.Count(x => x.Distance(ally) < this.settings.Distance) < this.settings.EnemiesCount)
                {
                    continue;
                }

                return this.Ability.UseAbility(ally);
            }

            return false;
        }
    }
}