namespace O9K.Hud.Modules.Screen.Time
{
    using System;
    using System.ComponentModel.Composition;
    using System.Globalization;

    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage.SDK.Renderer;
    using Ensage.SDK.Renderer.Texture;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class SystemTime : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly MenuVectorSlider position;

        private readonly MenuSlider textSize;

        private readonly string timeFormat;

        [ImportingConstructor]
        public SystemTime(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var timeMenu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Time"));
            timeMenu.AddTranslation(Lang.Ru, "Время");
            timeMenu.AddTranslation(Lang.Cn, "时间");

            var menu = timeMenu.Add(new Menu("System time"));
            menu.AddTranslation(Lang.Ru, "Системное время");
            menu.AddTranslation(Lang.Cn, "系统时间");

            this.enabled = menu.Add(new MenuSwitcher("Enabled", false).SetTooltip("Show your pc's time"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Показывать время пк");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "显示您的电脑时间");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.textSize = settings.Add(new MenuSlider("Size", 16, 10, 35));
            this.textSize.AddTranslation(Lang.Ru, "Размер");
            this.textSize.AddTranslation(Lang.Cn, "大小");

            this.position = new MenuVectorSlider(settings, new Vector2(Hud.Info.ScreenSize.X, Hud.Info.ScreenSize.Y * 0.0408f));

            this.timeFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern;
        }

        public void Activate()
        {
            this.context.Renderer.TextureManager.LoadFromDota(
                "o9k.time_bg",
                @"panorama\images\masks\gradient_rightleft_png.vtex_c",
                new TextureProperties
                {
                    Brightness = -180,
                    ColorRatio = new Vector4(0f, 0f, 0f, 0.8f)
                });

            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.position.Dispose();
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.context.Renderer.Draw += this.OnDraw;
            }
            else
            {
                this.context.Renderer.Draw -= this.OnDraw;
            }
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                var time = DateTime.Now.ToString(this.timeFormat);
                var timeSize = renderer.MeasureText(time, this.textSize);

                var bgWidth = timeSize.X * 2.5f;
                var bgPosition = this.position - new Vector2(bgWidth, 0);
                var textPosition = this.position - new Vector2(timeSize.X + (4 * Hud.Info.ScreenRatio), 0);

                renderer.DrawTexture("o9k.time_bg", bgPosition, new Vector2(bgWidth, this.textSize * 1.25f));
                renderer.DrawText(textPosition, time, Color.LightGray, this.textSize);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}