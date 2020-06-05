namespace O9K.Hud.Helpers.Notificator
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;

    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Input;
    using Core.Managers.Input.EventArgs;
    using Core.Managers.Input.Keys;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;
    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Texture;

    using MainMenu;

    using Modules;

    using Notifications;

    using SharpDX;

    using Color = System.Drawing.Color;

    [Export(typeof(INotificator))]
    internal class Notificator : IHudModule, INotificator
    {
        private const int MaxNotifications = 3;

        private readonly IContext9 context;

        private readonly MenuSwitcher debug;

        private readonly IInputManager9 inputManager;

        private readonly IMinimap minimap;

        private readonly List<Notification> notifications = new List<Notification>();

        private readonly MenuSlider position;

        private readonly Queue<Notification> queue = new Queue<Notification>();

        private readonly MenuSlider size;

        private Rectangle9 panel;

        private IUpdateHandler updateHandler;

        [ImportingConstructor]
        public Notificator(IContext9 context, IInputManager9 inputManager, IMinimap minimap, IHudMenu hudMenu)
        {
            this.context = context;
            this.inputManager = inputManager;
            this.minimap = minimap;

            var settings = hudMenu.NotificationsSettingsMenu;
            this.debug = settings.Add(new MenuSwitcher("Debug", false));
            this.debug.SetTooltip("Use this to adjust side notification messages");
            this.debug.AddTranslation(Lang.Ru, "Проверка");
            this.debug.AddTooltipTranslation(Lang.Ru, "Использовать для настройки");
            this.debug.AddTranslation(Lang.Cn, "调试");
            this.debug.AddTooltipTranslation(Lang.Cn, "使用它来调整侧面通知消息");
            this.debug.DisableSave();

            this.size = settings.Add(new MenuSlider("Size", "size", 65, 50, 100));
            this.size.AddTranslation(Lang.Ru, "Размер");
            this.size.AddTranslation(Lang.Cn, "大小");

            this.position = settings.Add(
                new MenuSlider("Position", "position", (int)(Hud.Info.ScreenSize.Y * 0.7f), 0, (int)Hud.Info.ScreenSize.Y));
            this.position.AddTranslation(Lang.Ru, "Позиция");
            this.position.AddTranslation(Lang.Cn, "位置");
        }

        public void Activate()
        {
            this.LoadTextures();

            this.updateHandler = UpdateManager.Subscribe(this.OnUpdate, 300, false);

            this.debug.ValueChange += this.DebugOnValueChange;
            this.size.ValueChange += this.SizeOnValueChange;
            this.position.ValueChange += this.PositionOnValueChange;
        }

        public void Dispose()
        {
            this.debug.ValueChange -= this.DebugOnValueChange;
            this.context.Renderer.Draw -= this.OnDrawDebug;
            this.context.Renderer.Draw -= this.OnDraw;
            this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
            this.size.ValueChange -= this.SizeOnValueChange;
            this.position.ValueChange -= this.PositionOnValueChange;
            this.updateHandler.IsEnabled = false;
            this.queue.Clear();
        }

        public void PushNotification(Notification notification)
        {
            this.queue.Enqueue(notification);

            if (!this.updateHandler.IsEnabled)
            {
                this.updateHandler.IsEnabled = true;
                this.context.Renderer.Draw += this.OnDraw;
                this.inputManager.MouseKeyDown += this.OnMouseKeyDown;
            }
        }

        private void DebugOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.OnDrawDebug;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDrawDebug;
            }
        }

        private void LoadTextures()
        {
            var tm = this.context.Renderer.TextureManager;
            tm.LoadFromDota(
                "o9k.notification_bg",
                @"panorama\images\hud\reborn\bg_deathsummary_psd.vtex_c",
                new TextureProperties
                {
                    Brightness = 10
                });
            tm.LoadFromDota("o9k.gold", @"panorama\images\hud\reborn\gold_large_png.vtex_c");
            tm.LoadFromDota("o9k.ping", @"panorama\images\hud\reborn\ping_icon_default_psd.vtex_c");
            tm.LoadFromDota("o9k.outpost", @"panorama\images\hud\icon_outpost_psd.vtex_c");
            tm.LoadFromResource("o9k.rune_regen", "rune_regen.png");
            tm.LoadFromResource("o9k.rune_bounty", "rune_bounty.png");
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var drawPosition = this.panel;

                foreach (var notification in this.notifications)
                {
                    notification.Draw(renderer, drawPosition, this.minimap);
                    drawPosition += new Vector2(0, -(drawPosition.Height + 20));
                }
            }
            catch (InvalidOperationException)
            {
                //ignore
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnDrawDebug(IRenderer renderer)
        {
            try
            {
                var drawPosition = this.panel;

                for (var i = 0; i < MaxNotifications; i++)
                {
                    renderer.DrawRectangle(drawPosition, Color.White);
                    drawPosition += new Vector2(0, -(drawPosition.Height + 20));
                }
            }
            catch
            {
                //ignore
            }
        }

        private void OnMouseKeyDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.MouseKey != MouseKey.Left)
                {
                    return;
                }

                var drawPosition = this.panel;

                foreach (var notification in this.notifications)
                {
                    if (drawPosition.Contains(e.ScreenPosition))
                    {
                        if (notification.OnClick())
                        {
                            e.Process = false;
                        }

                        return;
                    }

                    drawPosition += new Vector2(0, -(drawPosition.Height + 20));
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }

        private void OnUpdate()
        {
            try
            {
                this.notifications.RemoveAll(x => x.IsExpired);

                var count = Math.Min(this.queue.Count, MaxNotifications - this.notifications.Count);
                for (var i = 0; i < count; i++)
                {
                    var notification = this.queue.Dequeue();
                    notification.Pushed();
                    this.notifications.Insert(0, notification);
                }

                if (this.notifications.Count == 0)
                {
                    this.updateHandler.IsEnabled = false;
                    this.context.Renderer.Draw -= this.OnDraw;
                    this.inputManager.MouseKeyDown -= this.OnMouseKeyDown;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void PositionOnValueChange(object sender, SliderEventArgs e)
        {
            this.panel.Location = new Vector2(Hud.Info.ScreenSize.X - this.panel.Width, e.NewValue);
        }

        private void SizeOnValueChange(object sender, SliderEventArgs e)
        {
            this.panel.Size = new Size2F(e.NewValue * 3.5f, e.NewValue);
            this.panel.X = Hud.Info.ScreenSize.X - this.panel.Width;
        }
    }
}