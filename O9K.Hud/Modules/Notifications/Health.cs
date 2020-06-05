namespace O9K.Hud.Modules.Notifications
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Helpers.Notificator;
    using Helpers.Notificator.Notifications;

    using MainMenu;

    internal class Health : IHudModule
    {
        private readonly MenuSwitcher enabled;

        private readonly MenuSlider hpThreshold;

        private readonly MenuSwitcher moveCamera;

        private readonly INotificator notificator;

        private readonly MultiSleeper sleeper = new MultiSleeper();

        private Team ownerTeam;

        [ImportingConstructor]
        public Health(IHudMenu hudMenu, INotificator notificator)
        {
            this.notificator = notificator;

            var menu = hudMenu.NotificationsMenu.Add(new Menu("Health"));
            menu.AddTranslation(Lang.Ru, "Здоровье");
            menu.AddTranslation(Lang.Cn, "生命值");

            this.enabled = menu.Add(new MenuSwitcher("Enabled", false)).SetTooltip("Notify on low enemy health");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Оповещать о низком здоровье врага");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "通知敌人血量极低");

            this.moveCamera = menu.Add(new MenuSwitcher("Move camera")).SetTooltip("Move camera when clicked");
            this.moveCamera.AddTranslation(Lang.Ru, "Двигать камеру");
            this.moveCamera.AddTooltipTranslation(Lang.Ru, "Двигать камеру при нажатии");
            this.moveCamera.AddTranslation(Lang.Cn, "移动视野");
            this.moveCamera.AddTooltipTranslation(Lang.Cn, "单击时移动视角");

            this.hpThreshold = menu.Add(new MenuSlider("Health%", 30, 5, 60));
            this.hpThreshold.AddTranslation(Lang.Ru, "Здоровье%");
            this.hpThreshold.AddTranslation(Lang.Cn, "生命值％");
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;
            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            EntityManager9.UnitMonitor.UnitHealthChange -= this.OnUnitHealthChange;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                EntityManager9.UnitMonitor.UnitHealthChange += this.OnUnitHealthChange;
            }
            else
            {
                EntityManager9.UnitMonitor.UnitHealthChange -= this.OnUnitHealthChange;
            }
        }

        private void OnUnitHealthChange(Unit9 unit, float health)
        {
            try
            {
                if (!unit.IsHero || unit.IsIllusion || unit.Team == this.ownerTeam)
                {
                    return;
                }

                if (this.sleeper.IsSleeping(unit.Handle) || unit.Distance(Hud.CameraPosition) < 900)
                {
                    return;
                }

                var hpPct = (health / unit.MaximumHealth) * 100;
                if (hpPct > this.hpThreshold)
                {
                    return;
                }

                this.notificator.PushNotification(new HealthNotification(unit, this.moveCamera));
                this.sleeper.Sleep(unit.Handle, 20);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}