namespace O9K.AIO.Heroes.Kunkka.Abilities
{
    using AIO.Abilities;
    using AIO.Abilities.Menus;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Geometry;

    using SharpDX;

    using TargetManager;

    internal class Torrent : DisableAbility
    {
        public Torrent(ActiveAbility ability)
            : base(ability)
        {
        }

        public Modifier Modifier { get; set; }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<XMarkOnlyMenu>(this);
            if (menu.XMarkOnly)
            {
                return false;
            }

            return base.CanHit(targetManager, comboMenu);
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new XMarkOnlyMenu(this.Ability, simplifiedName);
        }

        public bool ShouldReturn(ActiveAbility xReturn, Vector3 xMarkPosition)
        {
            if (this.Modifier?.IsValid != true)
            {
                return false;
            }

            if (xMarkPosition.Distance2D(this.Modifier.Owner.Position) > this.Ability.Radius)
            {
                return false;
            }

            var remainingTime = this.Ability.ActivationDelay - this.Modifier.ElapsedTime;
            var requiredTime = xReturn.GetCastDelay() + 0.2f;

            if (remainingTime > requiredTime)
            {
                return false;
            }

            return true;
        }

        public bool UseAbility(Vector3 position, TargetManager targetManager, Sleeper comboSleeper)
        {
            if (!this.Ability.UseAbility(position))
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
    }
}