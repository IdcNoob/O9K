namespace O9K.AIO.Heroes.DarkSeer.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    internal class WallOfReplica : AoeAbility
    {
        public WallOfReplica(ActiveAbility ability)
            : base(ability)
        {
        }

        public Vacuum Vacuum { get; set; }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (!base.CanHit(targetManager, comboMenu))
            {
                return false;
            }

            //todo fix hit count
            //if (!this.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu)))
            //{
            //    return false;
            //}

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
            if (this.Vacuum != null && this.Vacuum.CastTime + 1 > Game.RawGameTime)
            {
                return this.Ability.UseAbility(this.Vacuum.CastPosition);
            }

            return base.UseAbility(targetManager, comboSleeper, aoe);
        }
    }
}