namespace O9K.Hud.Modules.Notifications
{
    using System;
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

    internal class Runes : IHudModule
    {
        private readonly MenuSwitcher bountyEnabled;

        private readonly Sleeper bountySleeper = new Sleeper();

        private readonly INotificator notificator;

        private readonly MenuSwitcher playSound;

        private readonly MenuSwitcher powerEnabled;

        private readonly Sleeper powerSleeper = new Sleeper();

        private Vector3[] bountyPositions;

        private Vector3[] powerPositions;

        [ImportingConstructor]
        public Runes(INotificator notificator, IHudMenu hudMenu)
        {
            this.notificator = notificator;

            var menu = hudMenu.NotificationsMenu.Add(new Menu("Runes"));
            menu.AddTranslation(Lang.Ru, "Руны");
            menu.AddTranslation(Lang.Cn, "神符");

            this.bountyEnabled = menu.Add(new MenuSwitcher("Bounty")).SetTooltip("Notify about bounty rune spawn");
            this.bountyEnabled.AddTranslation(Lang.Ru, "Баунти");
            this.bountyEnabled.AddTooltipTranslation(Lang.Ru, "Оповещать об спавне баунти рун");
            this.bountyEnabled.AddTranslation(Lang.Cn, "赏金神符");
            this.bountyEnabled.AddTooltipTranslation(Lang.Cn, "通知有关赏金符文生成");

            this.powerEnabled = menu.Add(new MenuSwitcher("Power-up")).SetTooltip("Notify about default rune spawn");
            this.powerEnabled.AddTranslation(Lang.Ru, "Обычные");
            this.powerEnabled.AddTooltipTranslation(Lang.Ru, "Оповещать об спавне обычных рун");
            this.powerEnabled.AddTranslation(Lang.Cn, "强化神符");
            this.powerEnabled.AddTooltipTranslation(Lang.Cn, "通知默认符文生成");

            this.playSound = menu.Add(new MenuSwitcher("Play sound"));
            this.playSound.AddTranslation(Lang.Ru, "Со звуком");
            this.playSound.AddTranslation(Lang.Cn, "播放声音");
        }

        public void Activate()
        {
            this.bountyPositions = ObjectManager.GetEntities<Item>()
                .Where(x => x.NetworkName == "CDOTA_Item_RuneSpawner_Bounty")
                .Select(x => x.Position)
                .ToArray();

            this.powerPositions = ObjectManager.GetEntities<Item>()
                .Where(x => x.NetworkName == "CDOTA_Item_RuneSpawner_Powerup")
                .Select(x => x.Position)
                .ToArray();

            var sleep = Math.Max((3 * 60) - Game.GameTime, 0);

            this.bountySleeper.Sleep(sleep);
            this.powerSleeper.Sleep(sleep);

            this.bountyEnabled.ValueChange += this.BountyEnabledOnValueChange;
            this.powerEnabled.ValueChange += this.PowerEnabledOnValueChange;
        }

        public void Dispose()
        {
            this.powerEnabled.ValueChange -= this.PowerEnabledOnValueChange;
            this.bountyEnabled.ValueChange -= this.BountyEnabledOnValueChange;
            UpdateManager.Unsubscribe(this.BountyOnUpdate);
            UpdateManager.Unsubscribe(this.PowerOnUpdate);
        }

        private void BountyEnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.BountyOnUpdate, 1000);
            }
            else
            {
                UpdateManager.Unsubscribe(this.BountyOnUpdate);
            }
        }

        private void BountyOnUpdate()
        {
            if (this.bountySleeper.IsSleeping)
            {
                return;
            }

            if (Game.GameTime % GameData.BountyRuneRespawnTime > GameData.BountyRuneRespawnTime - 20)
            {
                this.notificator.PushNotification(new RuneNotification(true, this.playSound, this.bountyPositions));
                this.bountySleeper.Sleep(GameData.BountyRuneRespawnTime - 5);
            }
        }

        private void PowerEnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.Subscribe(this.PowerOnUpdate, 1000);
            }
            else
            {
                UpdateManager.Unsubscribe(this.PowerOnUpdate);
            }
        }

        private void PowerOnUpdate()
        {
            if (this.powerSleeper.IsSleeping)
            {
                return;
            }

            if (Game.GameTime % GameData.RuneRespawnTime > GameData.RuneRespawnTime - 15)
            {
                this.notificator.PushNotification(new RuneNotification(false, this.playSound, this.powerPositions));
                this.powerSleeper.Sleep(GameData.RuneRespawnTime - 5);
            }

            if (Game.GameTime > 15 * 60)
            {
                UpdateManager.Unsubscribe(this.PowerOnUpdate);
            }
        }
    }
}