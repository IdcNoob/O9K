namespace O9K.AIO.Heroes.StormSpirit.Modes
{
    using System;

    using AIO.Modes.Base;

    using Base;

    using Core.Entities.Abilities.Heroes.StormSpirit;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu.EventArgs;

    using Ensage;
    using Ensage.SDK.Renderer;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class ManaCalculatorMode : BaseMode
    {
        private readonly ManaCalculatorModeMenu menu;

        private readonly IRenderManager renderer;

        private BallLightning ballLightning;

        public ManaCalculatorMode(BaseHero baseHero, ManaCalculatorModeMenu menu, IRenderManager renderer)
            : base(baseHero)
        {
            this.menu = menu;
            this.renderer = renderer;
        }

        private BallLightning BallLightning
        {
            get
            {
                if (this.ballLightning?.IsValid != true)
                {
                    this.ballLightning = EntityManager9.GetAbility<BallLightning>(this.Owner.Hero);
                }

                return this.ballLightning;
            }
        }

        public void Disable()
        {
            this.menu.Enabled.ValueChange -= this.EnabledOnValueChanged;
        }

        public override void Dispose()
        {
            this.menu.Enabled.ValueChange -= this.EnabledOnValueChanged;
        }

        public void Enable()
        {
            this.menu.Enabled.ValueChange += this.EnabledOnValueChanged;
        }

        private void EnabledOnValueChanged(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                this.renderer.Draw += this.OnDraw;
            }
            else
            {
                this.renderer.Draw -= this.OnDraw;
            }
        }

        private void OnDraw(IRenderer renderer1)
        {
            try
            {
                if (this.BallLightning == null || this.ballLightning.Level <= 0 || !this.ballLightning.Owner.IsAlive)
                {
                    return;
                }

                var mousePosition = Game.MousePosition;
                var mp = this.menu.ShowRemainingMp
                             ? this.BallLightning.GetRemainingMana(mousePosition).ToString()
                             : this.BallLightning.GetRequiredMana(mousePosition).ToString();

                this.renderer.DrawText(
                    Game.MouseScreenPosition + (new Vector2(30, 30) * Hud.Info.ScreenRatio),
                    mp,
                    Color.White,
                    16 * Hud.Info.ScreenRatio);
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
            }
        }
    }
}