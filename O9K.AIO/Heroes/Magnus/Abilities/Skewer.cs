namespace O9K.AIO.Heroes.Magnus.Abilities
{
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage.SDK.Extensions;

    using TargetManager;

    internal class Skewer : UsableAbility
    {
        private Unit9 ally;

        public Skewer(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            this.ally = this.GetPreferedAlly(targetManager, comboMenu);
            return true;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            return true;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            return false;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new SkewerMenu(this.Ability, simplifiedName);
        }

        public Unit9 GetPreferedAlly(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<SkewerMenu>(this);

            var ally = targetManager.AllyHeroes.Where(x => !x.Equals(this.Owner) && menu.IsAllyEnabled(x.Name))
                .OrderBy(x => x.Distance(targetManager.Target))
                .FirstOrDefault(x => x.Distance(targetManager.Target) < this.Ability.CastRange + 600);

            if (menu.SkewerToTower && ally == null)
            {
                ally = EntityManager9.Units.Where(x => x.IsTower && x.IsAlly(this.Owner) && x.IsAlive)
                    .OrderBy(x => x.Distance(targetManager.Target))
                    .FirstOrDefault(x => x.Distance(targetManager.Target) < this.Ability.CastRange + 500);
            }

            return ally;
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!target.IsStunned)
            {
                return false;
            }

            if (!target.IsVisible || target.IsInvulnerable)
            {
                return false;
            }

            if (target.IsMagicImmune && !this.Ability.PiercesMagicImmunity(target))
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.ally == null)
            {
                return false;
            }

            var target = targetManager.Target;
            var angle = (target.Position - this.ally.Position).AngleBetween(this.Owner.Position - target.Position);

            if (angle > 40)
            {
                this.Owner.BaseUnit.Move(this.ally.Position.Extend2D(target.Position, this.ally.Distance(target) + 100));
                this.Sleeper.Sleep(0.1f);
                this.OrbwalkSleeper.Sleep(0.1f);
                return true;
            }

            if (!this.Ability.UseAbility(this.ally.Position.Extend2D(this.Owner.Position, 200)))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public bool UseAbilityOnTarget(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(targetManager.Target, HitChance.Low))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(this.Ability.GetHitTime(targetManager.Target));
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        internal bool UseAbilityIfCondition(
            TargetManager targetManager,
            Sleeper comboSleeper,
            ComboModeMenu menu,
            ReversePolarity polarity,
            bool blink,
            bool force)
        {
            if (blink || force || polarity == null)
            {
                return false;
            }

            var input = polarity.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
            input.Delay += this.Ability.CastPoint + 0.5f;
            input.Range += this.Ability.CastRange;
            input.CastRange = this.Ability.CastRange;
            input.SkillShotType = SkillShotType.Circle;
            var output = polarity.Ability.GetPredictionOutput(input);
            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < polarity.TargetsToHit(menu))
            {
                return false;
            }

            var blinkPosition = output.CastPosition;
            if (this.Owner.Distance(blinkPosition) > this.Ability.CastRange)
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(this.Ability.GetHitTime(targetManager.Target));
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return this.Ability.UseAbility(blinkPosition);
        }
    }
}