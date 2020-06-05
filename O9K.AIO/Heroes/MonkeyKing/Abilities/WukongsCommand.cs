namespace O9K.AIO.Heroes.MonkeyKing.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;

    using Modes.Combo;

    using TargetManager;

    internal class WukongsCommand : AoeAbility
    {
        public WukongsCommand(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (!base.CanHit(targetManager, comboMenu))
            {
                return false;
            }

            if (this.Owner.Distance(targetManager.Target) > this.Ability.Radius)
            {
                return false;
            }

            if (!this.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu)))
            {
                return false;
            }

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