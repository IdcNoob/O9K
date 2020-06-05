namespace O9K.AIO.Heroes.Earthshaker.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using TargetManager;

    internal class EchoSlam : DisableAbility
    {
        public EchoSlam(ActiveAbility ability)
            : base(ability)
        {
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

            if (this.Owner.Distance(targetManager.Target) > 350)
            {
                return false;
            }

            return true;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility())
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

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
    }
}