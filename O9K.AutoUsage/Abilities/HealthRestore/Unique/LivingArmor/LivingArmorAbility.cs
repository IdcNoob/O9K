namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.LivingArmor
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.treant_living_armor)]
    internal class LivingArmorAbility : HealthRestoreAbility
    {
        private readonly LivingArmorSettings settings;

        public LivingArmorAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new LivingArmorSettings(settings.Menu, healthRestore);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var lowMp = this.Owner.ManaPercentage < this.settings.MpThreshold;

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).OrderBy(x => x.Health).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).ToList();

            if (this.settings.UseOnTowers)
            {
                allies.AddRange(EntityManager9.Units.Where(x => x.IsTower && x.IsAlive && x.IsAlly(this.Owner)));
            }

            if (this.settings.UseOnRax)
            {
                allies.AddRange(EntityManager9.Units.Where(x => x.IsBarrack && x.IsAlive && x.IsAlly(this.Owner)));
            }

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && !ally.IsTower && !ally.IsBarrack && !this.settings.SelfOnly)
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

                if (!this.Ability.CanHit(ally))
                {
                    continue;
                }

                if (ally.BaseUnit.HasAnyModifiers(this.HealthRestore.RestoreModifierName))
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