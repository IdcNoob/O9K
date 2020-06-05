namespace O9K.AIO.Heroes.Magnus.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage.SDK.Helpers;

    using TargetManager;

    internal class ReversePolarity : DisableAbility
    {
        private Unit9 ally;

        private Skewer skewer;

        public ReversePolarity(ActiveAbility ability)
            : base(ability)
        {
        }

        public void AddSkewer(Skewer skewer)
        {
            this.skewer = skewer;
        }

        public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
        {
            if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
            {
                return false;
            }

            this.ally = this.skewer.GetPreferedAlly(targetManager, comboMenu);
            return true;
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (!base.CanHit(targetManager, comboMenu))
            {
                return false;
            }

            if (!this.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu)))
            {
                return false;
            }

            return true;
        }

        public bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper, ComboModeMenu comboModeMenu)
        {
            this.ally = this.skewer.GetPreferedAlly(targetManager, comboModeMenu);

            if (this.ally != null)
            {
                this.Owner.BaseUnit.Move(this.ally.Position);
                UpdateManager.BeginInvoke(() => this.Ability.UseAbility(), 150);
            }
            else
            {
                this.Ability.UseAbility();
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new UsableAbilityHitCountMenu(this.Ability, simplifiedName);
        }

        public int TargetsToHit(IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this);
            return menu.HitCount;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (this.ally != null)
            {
                this.Owner.BaseUnit.Move(this.ally.Position);
            }

            if (!this.Ability.UseAbility())
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }
    }
}