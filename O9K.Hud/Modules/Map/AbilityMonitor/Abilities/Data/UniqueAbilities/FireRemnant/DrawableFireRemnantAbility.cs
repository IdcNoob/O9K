namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.FireRemnant
{
    using Base;

    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    internal class DrawableFireRemnantAbility : DrawableAbility
    {
        private readonly float addedTime;

        public DrawableFireRemnantAbility()
        {
            this.addedTime = Game.RawGameTime;
        }

        public override bool Draw
        {
            get
            {
                return this.Unit.IsValid && !this.Unit.IsVisible;
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

                return this.Unit.IsValid && this.Unit.IsAlive;
            }
        }

        public Entity Owner { get; set; }

        public float ShowHeroUntil { get; set; }

        public Unit Unit { get; set; }

        public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToScreen(this.Position, 35 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            var pct = (int)(((Game.RawGameTime - this.addedTime) / this.Duration) * 100);
            renderer.DrawTexture("o9k.outline_red", position * 1.2f);
            renderer.DrawTexture("o9k.outline_black" + pct, position * 1.25f);
            renderer.DrawTexture(this.AbilityTexture, position);
        }

        public override void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
        {
            if (Game.RawGameTime > this.ShowHeroUntil || this.Owner.IsVisible)
            {
                return;
            }

            var position = minimap.WorldToMinimap(this.Position, 25 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.outline_red", position * 1.08f);
            renderer.DrawTexture(this.AbilityTexture, position);
        }
    }
}