namespace O9K.Hud.Modules.Notifications
{
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Data;
    using Core.Helpers;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Helpers.Notificator;
    using Helpers.Notificator.Notifications;

    using MainMenu;

    using SharpDX;

    internal class Outposts : IHudModule
    {
        private readonly MenuSwitcher enabled;

        private readonly INotificator notificator;

        private readonly MenuSwitcher playSound;

        private readonly Sleeper sleeper = new Sleeper();

        private Vector3[] positions;

        [ImportingConstructor]
        public Outposts(INotificator notificator, IHudMenu hudMenu)
        {
            this.notificator = notificator;

            var menu = hudMenu.NotificationsMenu.Add(new Menu("Outpost"));
            menu.AddTranslation(Lang.Ru, "Аванпост");
            menu.AddTranslation(Lang.Cn, "前哨");

            this.enabled = menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Notify to capture outpost");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Оповещать об захвате аванпостов");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "通知占领哨所");

            this.playSound = menu.Add(new MenuSwitcher("Play sound"));
            this.playSound.AddTranslation(Lang.Ru, "Со звуком");
            this.playSound.AddTranslation(Lang.Cn, "播放声音");
        }

        public void Activate()
        {
            this.positions = ObjectManager.GetEntities<Building>()
                .Where(x => x.NetworkName == "CDOTA_BaseNPC_Watch_Tower")
                .Select(x => x.Position)
                .ToArray();

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            UpdateManager.Unsubscribe(this.OnUpdate);
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.OnUpdate, 1000);
            }
            else
            {
                UpdateManager.Unsubscribe(this.OnUpdate);
            }
        }

        private void OnUpdate()
        {
            if (this.sleeper.IsSleeping)
            {
                return;
            }

            if (Game.GameTime % GameData.OutpostExperienceTime > GameData.OutpostExperienceTime - 30)
            {
                this.notificator.PushNotification(new OutpostNotification(this.playSound, this.positions));
                this.sleeper.Sleep(GameData.OutpostExperienceTime - 5);
            }
        }
    }
}