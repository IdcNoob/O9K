namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using SharpDX;

    internal class DrawableAbility : IDrawableAbility
    {
        protected ParticleEffect RangeParticle;

        public AbilityId AbilityId { get; set; }

        public string AbilityTexture { get; set; }

        public virtual bool Draw { get; } = true;

        public string HeroTexture { get; set; }

        public bool IsShowingRange { get; set; }

        public virtual bool IsValid
        {
            get
            {
                return Game.RawGameTime < this.ShowUntil;
            }
        }

        public string MinimapHeroTexture { get; set; }

        public Vector3 Position { get; set; }

        public float Range { get; set; }

        public Vector3 RangeColor { get; set; }

        public float ShowUntil { get; set; }

        public virtual void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToScreen(this.Position, 45 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.outline_red", position * 1.12f);
            renderer.DrawTexture(this.HeroTexture, position);

            var abilityTexturePosition = position * 0.5f;
            abilityTexturePosition.X += abilityTexturePosition.Width * 0.8f;
            abilityTexturePosition.Y += abilityTexturePosition.Height * 0.6f;

            renderer.DrawTexture("o9k.outline_green_pct100", abilityTexturePosition * 1.2f);
            renderer.DrawTexture(this.AbilityTexture, abilityTexturePosition);
        }

        public virtual void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToMinimap(this.Position, 25 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.outline_red", position * 1.08f);
            renderer.DrawTexture(this.MinimapHeroTexture, position);
        }

        public void DrawRange()
        {
            if (!this.IsShowingRange)
            {
                return;
            }

            if (this.RangeParticle != null)
            {
                this.RangeParticle.SetControlPoint(0, this.Position);
                return;
            }

            this.RangeParticle = new ParticleEffect("particles/ui_mouseactions/drag_selected_ring.vpcf", this.Position);
            this.RangeParticle.SetControlPoint(1, this.RangeColor);
            this.RangeParticle.SetControlPoint(2, new Vector3(-this.Range, 255, 0));
        }

        public virtual void RemoveRange()
        {
            if (this.RangeParticle?.IsValid == true)
            {
                this.RangeParticle.Dispose();
            }
        }
    }
}