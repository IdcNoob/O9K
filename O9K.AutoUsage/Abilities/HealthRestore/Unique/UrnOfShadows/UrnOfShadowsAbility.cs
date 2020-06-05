namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.UrnOfShadows
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    [AbilityId(AbilityId.item_urn_of_shadows)]
    [AbilityId(AbilityId.item_spirit_vessel)]
    internal class UrnOfShadowsAbility : HealthRestoreAbility
    {
        private readonly UrnOfShadowsSettings settings;

        public UrnOfShadowsAbility(IHealthRestore healthRestore, GroupSettings settings)
            : base(healthRestore)
        {
            this.settings = new UrnOfShadowsSettings(settings.Menu, healthRestore);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
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

                if (healthPercentage > this.settings.HpThreshold)
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

                if (enemies.Any(x => x.Distance(ally) < this.settings.Distance))
                {
                    continue;
                }

                return this.Ability.UseAbility(ally);
            }

            return false;
        }
    }
}