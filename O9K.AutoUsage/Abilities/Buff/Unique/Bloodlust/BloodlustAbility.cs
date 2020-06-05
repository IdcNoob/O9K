namespace O9K.AutoUsage.Abilities.Buff.Unique.Bloodlust
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

    [AbilityId(AbilityId.ogre_magi_bloodlust)]
    internal class BloodlustAbility : BuffAbility
    {
        private readonly BloodlustSettings settings;

        public BloodlustAbility(IBuff buff, GroupSettings settings)
            : base(buff)
        {
            this.settings = new BloodlustSettings(settings.Menu, buff);
        }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (this.Owner.ManaPercentage < this.settings.MpThreshold)
            {
                return false;
            }

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).ToList();

            if (this.settings.UseOnTowers)
            {
                allies.AddRange(EntityManager9.Units.Where(x => x.IsTower && x.IsAlive && x.IsAlly(this.Owner)));
            }

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && !ally.IsTower && !this.settings.SelfOnly)
                {
                    continue;
                }

                var selfTarget = ally.Equals(this.Owner);

                if (selfTarget && !this.Buff.BuffsOwner)
                {
                    continue;
                }

                if (!selfTarget && (!this.Buff.BuffsAlly || this.settings.SelfOnly))
                {
                    continue;
                }

                if (!this.Ability.CanHit(ally))
                {
                    continue;
                }

                var modifier = ally.BaseUnit.GetModifierByName(this.Buff.BuffModifierName);
                if (modifier != null)
                {
                    if (this.settings.RenewTime <= 0 || modifier.RemainingTime * 1000 > this.settings.RenewTime)
                    {
                        continue;
                    }
                }

                if (enemies.Count(x => x.Distance(ally) < this.settings.Distance) < this.settings.EnemiesCount)
                {
                    continue;
                }

                if (!this.settings.OnSight && (!this.settings.OnAttack || !ally.IsAttackingHero()))
                {
                    return false;
                }

                if (ally.HealthPercentage > this.settings.HpThreshold)
                {
                    continue;
                }

                return this.Ability.UseAbility(ally);
            }

            return false;
        }
    }
}