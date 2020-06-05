namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.Sunder
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Managers.Entity;

    using Ensage;

    using Settings;

    [AbilityId(AbilityId.terrorblade_sunder)]
    internal class SunderAbility : HealthRestoreAbility
    {
        private readonly SunderSettings settings;

        public SunderAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new SunderSettings(settings.Menu, healthRestore);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (this.Owner.HealthPercentage > this.settings.HpThreshold)
            {
                return false;
            }

            var enemies = heroes.Where(x => x.IsEnemy(this.Owner)).ToList();
            if (enemies.Count(x => x.Distance(this.Owner) < this.settings.Distance) < this.settings.EnemiesCount)
            {
                return false;
            }

            var possibleTargets = EntityManager9.Units
                .Where(
                    x => x.IsHero && x.IsAlive && x.IsVisible && !x.IsInvulnerable && !x.IsUntargetable
                         && x.HealthPercentage > this.settings.TargetHpThreshold && (x.IsAlly(this.Owner) || !x.IsBlockingAbilities)
                         && this.Ability.CanHit(x))
                .OrderByDescending(x => x.HealthPercentage)
                .ToList();

            var enemy = possibleTargets.Find(x => x.IsEnemy(this.Owner));
            if (enemy != null)
            {
                return this.Ability.UseAbility(enemy);
            }

            if (this.settings.UseOnIllusions)
            {
                var illusion = possibleTargets.Find(x => x.IsIllusion);
                if (illusion != null)
                {
                    return this.Ability.UseAbility(illusion);
                }
            }

            if (this.settings.UseOnAllies)
            {
                var ally = possibleTargets.Find(x => x.IsAlly(this.Owner) && !x.Equals(this.Owner));
                if (ally != null)
                {
                    return this.Ability.UseAbility(ally);
                }
            }

            return false;
        }
    }
}