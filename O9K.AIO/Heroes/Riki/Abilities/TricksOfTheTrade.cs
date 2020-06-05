namespace O9K.AIO.Heroes.Riki.Abilities
{
    using System;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Modes.Combo;

    using TargetManager;

    using BaseTricksOfTheTrade = Core.Entities.Abilities.Heroes.Riki.TricksOfTheTrade;

    internal class TricksOfTheTrade : NukeAbility
    {
        private readonly BaseTricksOfTheTrade tricks;

        public TricksOfTheTrade(ActiveAbility ability)
            : base(ability)
        {
            this.tricks = (BaseTricksOfTheTrade)ability;
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            var menu = comboMenu.GetAbilitySettingsMenu<TricksOfTheTradeMenu>(this);

            if (menu.SmartUsage)
            {
                var target = targetManager.Target;

                if (this.Owner.HealthPercentage < 25)
                {
                    return true;
                }

                if (target.GetImmobilityDuration() > 1.5f)
                {
                    return true;
                }

                if (!target.IsRanged && target.Distance(this.Owner) < 200 && target.HealthPercentage < 40)
                {
                    return true;
                }

                var damage = this.Ability.GetDamage(target);
                var totalDamage = Math.Floor(this.Ability.Radius / target.Speed / this.tricks.AttackRate) * damage * 0.9f;
                if (totalDamage < target.Health)
                {
                    return false;
                }
            }

            return true;
        }

        public bool CancelChanneling(TargetManager targetManager)
        {
            if (this.Owner.HasAghanimsScepter)
            {
                return false;
            }

            if (!this.Ability.IsChanneling || !this.Ability.BaseAbility.IsChanneling)
            {
                return false;
            }

            var target = targetManager.Target;
            if (target.IsStunned || target.IsRooted)
            {
                return false;
            }

            if (target.Distance(this.Owner) < this.Ability.Radius)
            {
                return false;
            }

            return this.Owner.BaseUnit.Stop();
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new TricksOfTheTradeMenu(this.Ability, simplifiedName);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;
            if (!target.IsVisible)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (!this.ChainStun(target, true))
                {
                    return false;
                }
            }

            var damage = this.Ability.GetDamage(target);
            if (damage <= 0)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.Owner.HasAghanimsScepter)
            {
                var enemies = targetManager.EnemyHeroes;
                var ally = targetManager.AllyHeroes
                    .Where(
                        x => (x.HealthPercentage < 50 || x.Equals(this.Owner)) && x.Distance(this.Owner) < this.Ability.CastRange
                                                                               && enemies.Any(z => z.Distance(x) < this.Ability.Radius))
                    .OrderByDescending(x => enemies.Count(z => z.Distance(x) < this.Ability.Radius))
                    .FirstOrDefault();

                if (ally == null)
                {
                    return false;
                }

                if (!this.Ability.UseAbility(ally))
                {
                    return false;
                }
            }
            else if (!this.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            if (this.Ability is IDisable disable)
            {
                targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, this.Ability.GetHitTime(targetManager.Target));
            }

            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}