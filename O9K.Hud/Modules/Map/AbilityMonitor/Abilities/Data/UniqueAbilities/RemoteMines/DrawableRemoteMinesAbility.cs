namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.RemoteMines
{
    using Base;

    using Core.Helpers;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using SharpDX;

    internal class DrawableRemoteMinesAbility : DrawableAbility
    {
        private readonly float addedTime;

        public DrawableRemoteMinesAbility()
        {
            this.addedTime = Game.RawGameTime;
        }

        public Entity BaseEntity { get; set; }

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
        }

        public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = Drawing.WorldToScreen(this.Position);
            if (position.IsZero)
            {
                return;
            }

            var time = Game.RawGameTime;

            if (time < this.ShowHeroUntil)
            {
                var heroTexturePosition = new Rectangle9(position, new Vector2(45));
                renderer.DrawTexture("o9k.outline_red", heroTexturePosition * 1.12f);
                renderer.DrawTexture(this.HeroTexture, heroTexturePosition);

                var abilityTexturePosition = new Rectangle9(position + new Vector2(30, 20), new Vector2(27));
                renderer.DrawTexture("o9k.outline_green_pct100", abilityTexturePosition * 1.2f);
                renderer.DrawTexture(this.AbilityTexture, abilityTexturePosition);
            }
            else
            {
                var pct = (int)(((time - this.addedTime) / this.Duration) * 100);
                var abilityTexturePosition = new Rectangle9(position - (new Vector2(35) / 2f), new Vector2(35));
                renderer.DrawTexture("o9k.outline_red", abilityTexturePosition * 1.2f);
                renderer.DrawTexture("o9k.outline_black" + pct, abilityTexturePosition * 1.25f);
                renderer.DrawTexture(this.AbilityTexture, abilityTexturePosition);
            }
        }

        public override void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
        {
            if (Game.RawGameTime > this.ShowHeroUntil || this.BaseEntity.IsVisible)
            {
                return;
            }

            var position = minimap.WorldToMinimap(this.Position, 25 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.outline_red", position * 1.08f);
            renderer.DrawTexture(this.MinimapHeroTexture, position);
        }
    }
}