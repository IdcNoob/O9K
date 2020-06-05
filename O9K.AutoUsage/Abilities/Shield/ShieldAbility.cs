namespace O9K.AutoUsage.Abilities.Shield
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Extensions;

    using Settings;

    internal class ShieldAbility : UsableAbility
    {
        private readonly ShieldSettings settings;

        public ShieldAbility(IShield shield, GroupSettings settings)
            : base(shield)
        {
            this.Shield = shield;
            this.settings = new ShieldSettings(settings.Menu, shield);
        }

        public ShieldAbility(IShield shield)
            : base(shield)
        {
            this.Shield = shield;
        }

        protected IShield Shield { get; }

        public override bool UseAbility(List<Unit9> heroes)
        {
            var lowMp = this.Owner.ManaPercentage < this.settings.MpThreshold;

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).OrderBy(x => x.Health).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner) && this.settings.IsEnemyHeroEnabled(x.Name))
                .ToList();

            if (this.Owner.IsIllusion && this.Shield.ShieldsOwner)
            {
                allies.Add(this.Owner);
            }

            foreach (var ally in allies)
            {
                if (!this.settings.SelfOnly && !this.settings.IsAllyHeroEnabled(ally.Name))
                {
                    continue;
                }

                if (this.settings.OnChannel && ally.IsChanneling)
                {
                    var ability = ally.Abilities.FirstOrDefault(x => x.IsChanneling);
                    if (ability == null || ability.Id == AbilityId.lion_mana_drain || ability.Id == AbilityId.windrunner_powershot
                        || ability.Id == AbilityId.oracle_fortunes_end)
                    {
                        continue;
                    }
                }
                else
                {
                    var healthPercentage = ally.HealthPercentage;

                    if (healthPercentage > this.settings.HpThreshold || (lowMp && healthPercentage > this.settings.CriticalHpThreshold))
                    {
                        continue;
                    }
                }

                var selfTarget = ally.Equals(this.Owner);

                if (selfTarget && !this.Shield.ShieldsOwner)
                {
                    continue;
                }

                if (!selfTarget && (!this.Shield.ShieldsAlly || this.settings.SelfOnly))
                {
                    continue;
                }

                if (!this.Ability.CanHit(ally, allies, this.settings.AlliesCount))
                {
                    continue;
                }

                if (ally.BaseUnit.HasModifier(this.Shield.ShieldModifierName))
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