namespace O9K.Core.Managers.Menu
{
    using Ensage;

    using Helpers;

    using SharpDX;

    using Color = System.Drawing.Color;

    public class MenuStyle
    {
        public MenuStyle()
        {
            if (Drawing.RenderMode == RenderMode.Dx11)
            {
                this.BackgroundColor = Color.FromArgb(175, 5, 5, 5);
                this.MenuSplitLineColor = Color.FromArgb(75, 255, 255, 255);
                this.HeaderBackgroundColor = Color.FromArgb(200, 3, 3, 3);
                this.MenuSplitLineSize = 0.1f;
            }
            else
            {
                this.BackgroundColor = Color.FromArgb(90, 5, 5, 5);
                this.MenuSplitLineColor = Color.FromArgb(10, 255, 255, 255);
                this.HeaderBackgroundColor = Color.FromArgb(140, 3, 3, 3);
                this.MenuSplitLineSize = 1f;
            }
        }

        public Color BackgroundColor { get; }

        public string Font { get; } = "Calibri";

        public Color HeaderBackgroundColor { get; }

        public float Height { get; } = 35 * Hud.Info.ScreenRatio;

        public float LeftIndent { get; } = 10 * Hud.Info.ScreenRatio;

        public Color MenuSplitLineColor { get; }

        public float MenuSplitLineSize { get; }

        public float RightIndent { get; } = 10 * Hud.Info.ScreenRatio;

        public float TextSize { get; } = 16 * Hud.Info.ScreenRatio;

        public float TextureAbilitySize { get; } = 27 * Hud.Info.ScreenRatio;

        public string TextureArrowKey { get; } = "menu_arrow";

        public float TextureArrowSize { get; } = 12 * Hud.Info.ScreenRatio;

        public Vector2 TextureHeroSize { get; } = new Vector2(35, 25) * Hud.Info.ScreenRatio;

        public string TextureIconKey { get; } = "menu_icon";

        public string TextureLeftArrowKey { get; } = "menu_right_arrow";

        public float TooltipTextSize { get; } = 14 * Hud.Info.ScreenRatio;
    }
}