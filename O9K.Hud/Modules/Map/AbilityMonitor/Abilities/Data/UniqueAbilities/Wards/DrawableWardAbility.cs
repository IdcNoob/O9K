namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wards
{
    using System;
    using System.Drawing;

    using Base;

    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    internal class DrawableWardAbility : DrawableAbility
    {
        public DrawableWardAbility()
        {
            this.AddedTime = Game.RawGameTime;
        }

        public string AbilityUnitName { get; set; }

        public float AddedTime { get; set; }

        public override bool Draw
        {
            get
            {
                return this.Unit == null || (this.Unit.IsValid && !this.Unit.IsVisible);
            }
        }

        public float Duration { get; set; }

        public override bool IsValid
        {
            get
            {
                if (Game.RawGameTime > this.ShowUntil)
                {
                    return false;
                }

                return this.Unit == null || (this.Unit.IsValid && this.Unit.IsAlive);
            }
        }

        public float ShowHeroUntil { get; set; }

        public Unit Unit { get; set; }

        public void AddUnit(Unit unit)
        {
            this.Unit = unit;
            this.Position = unit.Position;

            this.DrawRange();
        }

        public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToScreen(this.Position, 30 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            var pct = (int)(((Game.RawGameTime - this.AddedTime) / this.Duration) * 100);
            renderer.DrawTexture("o9k.outline_red", position * 1.2f);
            renderer.DrawTexture("o9k.outline_black" + pct, position * 1.25f);
            renderer.DrawTexture(this.AbilityTexture, position);

            position.Y += 30 * Hud.Info.ScreenRatio;
            position *= 2;

            renderer.DrawText(
                position,
                TimeSpan.FromSeconds(this.Duration - (Game.RawGameTime - this.AddedTime)).ToString(@"m\:ss"),
                Color.White,
                RendererFontFlags.Center | RendererFontFlags.VerticalCenter,
                18 * Hud.Info.ScreenRatio);
        }

        public override void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
        {
            if (this.Unit?.IsVisible == true)
            {
                return;
            }

            var position = minimap.WorldToMinimap(this.Position, 15 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.minimap_" + this.AbilityTexture, position);
        }
    }
}