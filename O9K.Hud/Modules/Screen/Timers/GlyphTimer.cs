namespace O9K.Hud.Modules.Screen.Timers
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using MainMenu;

    using SharpDX;

    using Drawer = Helpers.Drawer;

    internal class GlyphTimer : IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher enabled;

        private readonly MenuSwitcher hide;

        private readonly MenuSwitcher showRemaining;

        private readonly MenuVectorSlider textPosition;

        private readonly MenuSlider textSize;

        private Team ownerTeam;

        [ImportingConstructor]
        public GlyphTimer(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var timersMenu = hudMenu.ScreenMenu.GetOrAdd(new Menu("Timers"));
            timersMenu.AddTranslation(Lang.Ru, "Таймеры");
            timersMenu.AddTranslation(Lang.Cn, "计时 器");

            var menu = timersMenu.Add(new Menu("Glyph timer"));
            menu.AddTranslation(Lang.Ru, "Таймер глифа");
            menu.AddTranslation(Lang.Cn, "防御符文计数器");

            this.enabled = menu.Add(new MenuSwitcher("Enabled"));
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTranslation(Lang.Cn, "启用");

            this.showRemaining = menu.Add(new MenuSwitcher("Remaining time")).SetTooltip("Show remaining time or ready time");
            this.showRemaining.AddTranslation(Lang.Ru, "Оставшееся время");
            this.showRemaining.AddTooltipTranslation(Lang.Ru, "Показывать оставшееся время или время готовности");
            this.showRemaining.AddTranslation(Lang.Cn, "剩余时间");
            this.showRemaining.AddTooltipTranslation(Lang.Cn, "显示剩余时间或准备时间");

            this.hide = menu.Add(new MenuSwitcher("Auto hide")).SetTooltip("Hide timer if glyph is ready");
            this.hide.AddTranslation(Lang.Ru, "Прятать автоматически");
            this.hide.AddTooltipTranslation(Lang.Ru, "Спрятать, если глиф готов");
            this.hide.AddTranslation(Lang.Cn, "自动隐藏");
            this.hide.AddTooltipTranslation(Lang.Cn, "如果字形已准备好，则隐藏计时器");

            var settings = menu.Add(new Menu("Settings"));
            settings.AddTranslation(Lang.Ru, "Настройки");
            settings.AddTranslation(Lang.Cn, "设置");

            this.textSize = settings.Add(new MenuSlider("Size", 15, 10, 35));
            this.textSize.AddTranslation(Lang.Ru, "Размер");
            this.textSize.AddTranslation(Lang.Cn, "大小");

            this.textPosition = new MenuVectorSlider(settings, Hud.Info.GlyphPosition + new Vector2(0, 10));
        }

        public void Activate()
        {
            this.ownerTeam = EntityManager9.Owner.Team;
            this.enabled.ValueChange += this.EnabledOnValueChange;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.context.Renderer.Draw -= this.OnDraw;
            this.textPosition.Dispose();
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
                var cd = this.ownerTeam == Team.Radiant ? Game.GlyphCooldownDire : Game.GlyphCooldownRadiant;

                if (cd > 0)
                {
                    if (!this.showRemaining)
                    {
                        cd += Game.GameTime;
                    }

                    Drawer.DrawTextWithBackground(TimeSpan.FromSeconds(cd).ToString(@"m\:ss"), this.textSize, this.textPosition, renderer);
                }
                else if (!this.hide)
                {
                    Drawer.DrawTextWithBackground("Ready", this.textSize, this.textPosition, renderer);
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}