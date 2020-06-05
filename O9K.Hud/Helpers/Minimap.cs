namespace O9K.Hud.Helpers
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Helpers;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    using MainMenu;

    using Modules;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal interface IMinimap
    {
        RectangleF GetMinimap();

        Vector2 WorldToMinimap(Vector3 position);

        Rectangle9 WorldToMinimap(Vector3 position, float size);

        Rectangle9 WorldToScreen(Vector3 position, float size);
    }

    [Export(typeof(IMinimap))]
    internal class Minimap : IMinimap, IHudModule
    {
        private readonly IContext9 context;

        private readonly MenuSwitcher debug;

        private readonly float MapBottom = -7700;

        private readonly float MapLeft = -7700;

        private readonly float MapRight = 7700;

        private readonly float MapTop = 7700;

        private readonly MenuSlider xPosition;

        private readonly MenuSlider xSize;

        private readonly MenuSlider yPosition;

        private readonly MenuSlider ySize;

        private RectangleF minimap;

        private float minimapMapScaleX;

        private float minimapMapScaleY;

        [ImportingConstructor]
        public Minimap(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var minimapSize = new Vector2(Hud.Info.ScreenSize.X * 0.127f, Hud.Info.ScreenSize.Y * 0.226f);
            if (Game.GetConsoleVar("dota_hud_extra_large_minimap").GetInt() == 1)
            {
                minimapSize *= 1.145f;
            }

            var settings = hudMenu.MinimapSettingsMenu;
            this.debug = settings.Add(new MenuSwitcher("Debug", false));
            this.debug.SetTooltip("Use this to adjust minimap position and size");
            this.debug.AddTranslation(Lang.Ru, "Проверка");
            this.debug.AddTooltipTranslation(Lang.Ru, "Использовать для настройки");
            this.debug.AddTranslation(Lang.Cn, "调试");
            this.debug.AddTooltipTranslation(Lang.Cn, "使用它来调整小地图的位置和大小");
            this.debug.DisableSave();

            this.xPosition = settings.Add(new MenuSlider("X coordinate", "x", 0, 0, (int)Hud.Info.ScreenSize.X));
            this.xPosition.AddTranslation(Lang.Ru, "X координата");
            this.xPosition.AddTranslation(Lang.Cn, "X位置");

            this.yPosition = settings.Add(
                new MenuSlider("Y coordinate", "y", (int)(Hud.Info.ScreenSize.Y - minimapSize.Y), 0, (int)Hud.Info.ScreenSize.Y));
            this.yPosition.AddTranslation(Lang.Ru, "Y координата");
            this.yPosition.AddTranslation(Lang.Cn, "Y位置");

            this.xSize = settings.Add(new MenuSlider("X Size", "xSize", (int)minimapSize.X, 0, 600));
            this.xSize.AddTranslation(Lang.Ru, "X размер");
            this.xSize.AddTranslation(Lang.Cn, "X大小");

            this.ySize = settings.Add(new MenuSlider("Y Size", "ySize", (int)minimapSize.Y, 0, 600));
            this.ySize.AddTranslation(Lang.Ru, "Y размер");
            this.ySize.AddTranslation(Lang.Cn, "Y大小");

            if (Game.GameMode == GameMode.Demo)
            {
                this.MapLeft = -3500;
                this.MapBottom = -3600;
                this.MapRight = 3000;
                this.MapTop = 2700;
            }
        }

        public void Activate()
        {
            this.debug.ValueChange += this.DebugOnValueChange;
            this.xPosition.ValueChange += this.XPositionOnValueChange;
            this.yPosition.ValueChange += this.YPositionOnValueChange;
            this.xSize.ValueChange += this.XSizeOnValueChange;
            this.ySize.ValueChange += this.YSizeOnValueChange;
        }

        public void Dispose()
        {
            this.context.Renderer.Draw -= this.OnDrawDebug;
            this.debug.ValueChange -= this.DebugOnValueChange;
            this.xPosition.ValueChange -= this.XPositionOnValueChange;
            this.yPosition.ValueChange -= this.YPositionOnValueChange;
            this.xSize.ValueChange -= this.XSizeOnValueChange;
            this.ySize.ValueChange -= this.YSizeOnValueChange;
        }

        public RectangleF GetMinimap()
        {
            return this.minimap;
        }

        public Vector2 WorldToMinimap(Vector3 position)
        {
            var x = position.X - this.MapLeft;
            var y = position.Y - this.MapBottom;

            var scaledX = x * this.minimapMapScaleX;
            var scaledY = y * this.minimapMapScaleY;

            var screenX = this.minimap.X + scaledX;
            var screenY = this.minimap.Bottom - scaledY;

            var minimapPosition = new Vector2(screenX, screenY);

            if (!this.minimap.Contains(minimapPosition))
            {
                return Vector2.Zero;
            }

            return minimapPosition;
        }

        public Rectangle9 WorldToMinimap(Vector3 position, float size)
        {
            var x = position.X - this.MapLeft;
            var y = position.Y - this.MapBottom;

            var scaledX = x * this.minimapMapScaleX;
            var scaledY = y * this.minimapMapScaleY;

            var screenX = this.minimap.X + scaledX;
            var screenY = this.minimap.Bottom - scaledY;

            var minimapPosition = new Vector2(screenX, screenY);

            if (!this.minimap.Contains(minimapPosition))
            {
                return Rectangle9.Zero;
            }

            return new Rectangle9(minimapPosition - (size / 2f), size, size);
        }

        public Rectangle9 WorldToScreen(Vector3 position, float size)
        {
            var mapPosition = Drawing.WorldToScreen(position);
            if (mapPosition.IsZero)
            {
                return Rectangle9.Zero;
            }

            return new Rectangle9(mapPosition - (size / 2f), size, size);
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

        private void OnDrawDebug(IRenderer renderer)
        {
            try
            {
                renderer.DrawRectangle(this.minimap, Color.White, 2);
                renderer.DrawCircle(this.WorldToMinimap(Game.MousePosition), 2, Color.White);

                foreach (var tower in EntityManager9.Units.Where(x => x.IsTower))
                {
                    renderer.DrawCircle(this.WorldToMinimap(tower.Position), 6, Color.White);
                }
            }
            catch
            {
                //ignore
            }
        }

        private void XPositionOnValueChange(object sender, SliderEventArgs e)
        {
            this.minimap.X = e.NewValue;
        }

        private void XSizeOnValueChange(object sender, SliderEventArgs e)
        {
            this.minimap.Width = e.NewValue;
            this.minimapMapScaleX = this.minimap.Width / (Math.Abs(this.MapLeft) + Math.Abs(this.MapRight));
        }

        private void YPositionOnValueChange(object sender, SliderEventArgs e)
        {
            this.minimap.Y = e.NewValue;
        }

        private void YSizeOnValueChange(object sender, SliderEventArgs e)
        {
            this.minimap.Height = e.NewValue;
            this.minimapMapScaleY = this.minimap.Height / (Math.Abs(this.MapBottom) + Math.Abs(this.MapTop));
        }
    }
}