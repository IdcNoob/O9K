namespace O9K.Hud.Helpers
{
    using System.ComponentModel.Composition;

    using Core.Helpers;
    using Core.Managers.Context;
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

    internal interface ITopPanel
    {
        Rectangle9 GetPlayerPosition(int id);

        Rectangle9 GetPlayersHealthBarPosition(int id, float height, float topIndent);

        Rectangle9 GetPlayersUltimatePosition(int id, float size, float topIndent);

        Rectangle9 GetScorePosition(Team team);

        Rectangle9 GetTimePosition();
    }

    [Export(typeof(ITopPanel))]
    internal class TopPanel : IHudModule, ITopPanel
    {
        private const int PlayersPerPanel = 5;

        private readonly MenuSlider botPosition;

        private readonly MenuSlider centerPosition;

        private readonly IContext9 context;

        private readonly MenuSwitcher debug;

        private readonly MenuSlider sidePosition;

        private Rectangle9 centerPanel;

        private Rectangle9 leftPanel;

        private Rectangle9 rightPanel;

        private float widthPerPlayer;

        [ImportingConstructor]
        public TopPanel(IContext9 context, IHudMenu hudMenu)
        {
            this.context = context;

            var centerIndent = Hud.Info.ScreenSize.X * 0.053f;
            var size = new Size2F(Hud.Info.ScreenSize.X * 0.165f, Hud.Info.ScreenSize.Y * 0.037f);

            var settings = hudMenu.TopPanelSettingsMenu;
            this.debug = settings.Add(new MenuSwitcher("Debug", false));
            this.debug.SetTooltip("Use this to adjust top panel position and size");
            this.debug.AddTranslation(Lang.Ru, "Проверка");
            this.debug.AddTooltipTranslation(Lang.Ru, "Использовать для настройки");
            this.debug.AddTranslation(Lang.Cn, "调试");
            this.debug.AddTooltipTranslation(Lang.Cn, "用它来调整顶板的位置和大小");
            this.debug.DisableSave();

            this.centerPosition = settings.Add(
                new MenuSlider("Center position", "center", (int)centerIndent, 0, (int)(Hud.Info.ScreenSize.X / 4)));
            this.centerPosition.AddTranslation(Lang.Ru, "Центральное положение");
            this.centerPosition.AddTranslation(Lang.Cn, "中间位置");

            this.sidePosition = settings.Add(
                new MenuSlider("Side position", "side", (int)size.Width, 100, (int)(Hud.Info.ScreenSize.X / 2)));
            this.sidePosition.AddTranslation(Lang.Ru, "Боковое положение");
            this.sidePosition.AddTranslation(Lang.Cn, "侧面");

            this.botPosition = settings.Add(new MenuSlider("Bottom position", "bot", (int)size.Height, 25, 100));
            this.botPosition.AddTranslation(Lang.Ru, "Нижнее положение");
            this.botPosition.AddTranslation(Lang.Cn, "底部");
        }

        public void Activate()
        {
            this.debug.ValueChange += this.DebugOnValueChange;
            this.centerPosition.ValueChange += this.CenterPositionOnValueChange;
            this.sidePosition.ValueChange += this.SidePositionOnValueChange;
            this.botPosition.ValueChange += this.BotPositionOnValueChange;
        }

        public void Dispose()
        {
            this.context.Renderer.Draw -= this.OnDrawDebug;
            this.debug.ValueChange -= this.DebugOnValueChange;
            this.sidePosition.ValueChange -= this.SidePositionOnValueChange;
            this.centerPosition.ValueChange -= this.CenterPositionOnValueChange;
            this.botPosition.ValueChange -= this.BotPositionOnValueChange;
        }

        public Rectangle9 GetPlayerPosition(int id)
        {
            var panel = id < PlayersPerPanel ? this.leftPanel : this.rightPanel;
            var position = new Rectangle9();

            if (id >= PlayersPerPanel)
            {
                id -= PlayersPerPanel;
            }

            position.Size = new Size2F(this.widthPerPlayer, panel.Height);
            position.Location = new Vector2(panel.Left + (position.Width * id), panel.Y);

            return position;
        }

        public Rectangle9 GetPlayersHealthBarPosition(int id, float height, float topIndent)
        {
            var panel = id < PlayersPerPanel ? this.leftPanel : this.rightPanel;
            var position = new Rectangle9();

            if (id >= PlayersPerPanel)
            {
                id -= PlayersPerPanel;
            }

            position.Size = new Size2F((this.widthPerPlayer) - 2, height);
            position.Location = new Vector2(panel.Left + ((position.Width + 2) * id) + 1, panel.Height + topIndent);

            return position;
        }

        public Rectangle9 GetPlayersUltimatePosition(int id, float size, float topIndent)
        {
            var panel = id < PlayersPerPanel ? this.leftPanel : this.rightPanel;
            var position = new Rectangle9();

            if (id >= PlayersPerPanel)
            {
                id -= PlayersPerPanel;
            }

            position.Size = new Size2F(size, size);
            position.Location = new Vector2(
                (panel.Left + (this.widthPerPlayer * id) + (this.widthPerPlayer / 2f)) - (size / 2f),
                (panel.Height - (size / 2f)) + topIndent);

            return position;
        }

        public Rectangle9 GetScorePosition(Team team)
        {
            if (team == Team.Radiant)
            {
                return new Rectangle9(this.centerPanel.X, this.centerPanel.Y, this.centerPanel.Width * 0.32f, this.centerPanel.Height);
            }
            else
            {
                return new Rectangle9(
                    this.centerPanel.Right - (this.centerPanel.Width * 0.31f),
                    this.centerPanel.Y,
                    this.centerPanel.Width * 0.32f,
                    this.centerPanel.Height);
            }
        }

        public Rectangle9 GetTimePosition()
        {
            return this.centerPanel * new Size2F(0.4f, 1f);
        }

        private void BotPositionOnValueChange(object sender, SliderEventArgs e)
        {
            this.leftPanel.Height = e.NewValue;
            this.rightPanel.Height = e.NewValue;
            this.centerPanel.Height = e.NewValue;
        }

        private void CenterPositionOnValueChange(object sender, SliderEventArgs e)
        {
            this.centerPanel.X = (Hud.Info.ScreenSize.X / 2f) - e.NewValue;
            this.centerPanel.Width = e.NewValue * 2;
            this.rightPanel.X = this.centerPanel.Right;
            this.leftPanel.X = (Hud.Info.ScreenSize.X / 2f) - (this.centerPanel.Width / 2f) - this.leftPanel.Width;
            this.widthPerPlayer = this.leftPanel.Width / PlayersPerPanel;
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
                renderer.DrawRectangle(this.leftPanel, Color.White, 2);
                renderer.DrawRectangle(this.rightPanel, Color.White, 2);
                renderer.DrawRectangle(this.centerPanel, Color.White, 2);
            }
            catch
            {
                //ignore
            }
        }

        private void SidePositionOnValueChange(object sender, SliderEventArgs e)
        {
            this.leftPanel.X = (Hud.Info.ScreenSize.X / 2f) - (this.centerPanel.Width / 2f) - e.NewValue;
            this.leftPanel.Width = e.NewValue;
            this.rightPanel.Width = e.NewValue;
            this.widthPerPlayer = this.leftPanel.Width / PlayersPerPanel;
        }
    }
}