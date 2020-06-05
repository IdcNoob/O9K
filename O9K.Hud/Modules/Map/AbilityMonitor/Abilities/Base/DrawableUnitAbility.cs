namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
    using System;

    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using SharpDX;

    internal class DrawableUnitAbility : IDrawableAbility
    {
        protected ParticleEffect RangeParticle;

        private readonly float addedTime;

        public DrawableUnitAbility()
        {
            this.addedTime = Game.RawGameTime;
        }

        public AbilityId AbilityId { get; set; }

        public string AbilityTexture { get; set; }

        public bool Draw
        {
            get
            {
                return !this.Unit.IsVisible;
            }
        }

        public float Duration { get; set; }

        public string HeroTexture { get; set; }

        public bool IsShowingRange { get; set; }

        public bool IsValid
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

        public string MinimapHeroTexture { get; set; }

        public Entity Owner { get; set; }

        public Vector3 Position { get; set; }

        public float Range { get; set; }

        public Vector3 RangeColor { get; set; }

        public float ShowHeroUntil { get; set; }

        public bool ShowTimer { get; set; }

        public float ShowUntil { get; set; }

        public Unit Unit { get; set; }

        public void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToScreen(this.Position, 45 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            var time = Game.RawGameTime;

            if (time < this.ShowHeroUntil)
            {
                renderer.DrawTexture("o9k.outline_red", position * 1.12f);
                renderer.DrawTexture(this.HeroTexture, position);

                var abilityTexturePosition = position * 0.5f;
                abilityTexturePosition.X += abilityTexturePosition.Width * 0.8f;
                abilityTexturePosition.Y += abilityTexturePosition.Height * 0.6f;

                renderer.DrawTexture("o9k.outline_green_pct100", abilityTexturePosition * 1.2f);
                renderer.DrawTexture(this.AbilityTexture, abilityTexturePosition);
            }
            else
            {
                renderer.DrawTexture("o9k.outline_red", position);

                if (this.ShowTimer)
                {
                    var pct = (int)(((time - this.addedTime) / this.Duration) * 100);
                    renderer.DrawTexture("o9k.outline_black" + Math.Min(pct, 100), position * 1.05f);
                }

                renderer.DrawTexture(this.AbilityTexture, position * 0.8f);
            }
        }

        public void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
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