namespace O9K.AutoUsage.Abilities.Buff
{
    using System.Collections.Generic;
    using System.Linq;

    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Ensage.SDK.Extensions;

    using Settings;

    internal class BuffAbility : UsableAbility
    {
        private readonly BuffSettings settings;

        public BuffAbility(IBuff buff, GroupSettings settings)
            : base(buff)
        {
            this.Buff = buff;
            this.settings = new BuffSettings(settings.Menu, buff);
        }

        public BuffAbility(IBuff buff)
            : base(buff)
        {
            this.Buff = buff;
        }

        protected IBuff Buff { get; }

        public override bool UseAbility(List<Unit9> heroes)
        {
            if (this.Owner.ManaPercentage < this.settings.MpThreshold)
            {
                return false;
            }

            var allies = heroes.Where(x => !x.IsInvulnerable && x.IsAlly(this.Owner)).ToList();
            var enemies = heroes.Where(x => !x.IsInvulnerable && x.IsEnemy(this.Owner)).ToList();

            if (this.Owner.IsIllusion && this.Buff.BuffsOwner)
            {
                allies.Add(this.Owner);
            }

            foreach (var ally in allies)
            {
                if (!this.settings.IsHeroEnabled(ally.Name) && !this.settings.SelfOnly)
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

                if (!this.Ability.CanHit(ally, allies, this.settings.AlliesCount))
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

                return this.Ability.UseAbility(ally, allies, HitChance.Medium, this.settings.AlliesCount);
            }

            return false;
        }
    }
}