namespace O9K.Evader.Helpers
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Renderer.Utils;

    using Ensage.SDK.Renderer;

    using Metadata;

    using Pathfinder;

    using Settings;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class StateDrawer : IEvaderService
    {
        private readonly IContext9 context;

        private readonly HotkeysMenu hotkeysMenu;

        private readonly RectangleF startPosition;

        private readonly Sleeper stateSleeper = new Sleeper(10);

        private readonly float textSize;

        private Pathfinder.EvadeMode pathfinderMode;

        private bool showBkb;

        private bool showProactive;

        [ImportingConstructor]
        public StateDrawer(IMainMenu menu, IContext9 context)
        {
            this.hotkeysMenu = menu.Hotkeys;
            this.context = context;

            this.textSize = 22 * Hud.Info.ScreenRatio;
            this.startPosition = new Rectangle9(0, Hud.Info.ScreenSize.Y * 0.05f, Hud.Info.ScreenSize.X * 0.995f, this.textSize);
        }

        public LoadOrder LoadOrder { get; } = LoadOrder.StateDrawer;

        public void Activate()
        {
            this.hotkeysMenu.BkbEnabled.ValueChange += this.BkbEnabledOnValueChanged;
            this.hotkeysMenu.PathfinderMode.ValueChange += this.PathfinderModeOnValueChanged;
            this.hotkeysMenu.ProactiveEvade.ValueChange += this.ProactiveEvadeOnValueChange;

            this.context.Renderer.Draw += this.RendererOnDraw;
        }

        public void Dispose()
        {
            this.hotkeysMenu.BkbEnabled.ValueChange -= this.BkbEnabledOnValueChanged;
            this.hotkeysMenu.PathfinderMode.ValueChange -= this.PathfinderModeOnValueChanged;
            this.hotkeysMenu.ProactiveEvade.ValueChange -= this.ProactiveEvadeOnValueChange;

            this.context.Renderer.Draw -= this.RendererOnDraw;
        }

        private void BkbEnabledOnValueChanged(object sender, KeyEventArgs e)
        {
            this.stateSleeper.Sleep(5);
            this.showBkb = e.NewValue;
        }

        private void PathfinderModeOnValueChanged(object sender, KeyEventArgs e)
        {
            this.stateSleeper.Sleep(5);

            if (!e.NewValue)
            {
                return;
            }

            if ((int)this.pathfinderMode >= Enum.GetNames(typeof(Pathfinder.EvadeMode)).Length - 1)
            {
                this.pathfinderMode = 0;
            }
            else
            {
                this.pathfinderMode++;
            }
        }

        private void ProactiveEvadeOnValueChange(object sender, KeyEventArgs e)
        {
            this.stateSleeper.Sleep(5);
            this.showProactive = e.NewValue;
        }

        private void RendererOnDraw(IRenderer renderer)
        {
            try
            {
                if (!this.hotkeysMenu.DrawState && !this.stateSleeper.IsSleeping)
                {
                    return;
                }

                var position = this.startPosition;

                if (this.showProactive)
                {
                    renderer.DrawText(position, "Evader (Proactive)", Color.OrangeRed, RendererFontFlags.Right, this.textSize);
                }
                else
                {
                    renderer.DrawText(position, "Evader", Color.LawnGreen, RendererFontFlags.Right, this.textSize);
                }

                position.Y += this.textSize;

                switch (this.pathfinderMode)
                {
                    case Pathfinder.EvadeMode.All:
                        renderer.DrawText(position, "Dodge", Color.LawnGreen, RendererFontFlags.Right, this.textSize);
                        break;
                    case Pathfinder.EvadeMode.Disables:
                        renderer.DrawText(position, "Dodge (Disables)", Color.OrangeRed, RendererFontFlags.Right, this.textSize);
                        break;
                    case Pathfinder.EvadeMode.None:
                        renderer.DrawText(position, "Dodge (None)", Color.Red, RendererFontFlags.Right, this.textSize);
                        break;
                }

                position.Y += this.textSize;

                renderer.DrawText(position, "BKB", this.showBkb ? Color.LawnGreen : Color.Red, RendererFontFlags.Right, this.textSize);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}