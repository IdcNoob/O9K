namespace O9K.Hud.Modules.Map.Teleports
{
    using System;

    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal sealed class Teleport
    {
        private const string AbilityTexture = nameof(AbilityId.item_tpscroll) + "_rounded";

        private readonly Color color;

        private readonly float displayUntil;

        private readonly float duration;

        private readonly string mapTexture;

        private readonly string minimapTexture;

        private readonly ParticleEffect particle;

        private readonly Vector3 teleportPosition;

        public Teleport(ParticleEffect particle, HeroId id, Vector3 position, float duration, bool start)
        {
            this.particle = particle;
            this.HeroId = id;
            this.teleportPosition = position;
            this.duration = duration;
            this.displayUntil = Game.RawGameTime + duration;
            this.mapTexture = this.HeroId + "_rounded";
            this.minimapTexture = this.HeroId + "_icon";
            this.color = start ? Color.DarkOrange : Color.Red;
        }

        public HeroId HeroId { get; }

        public bool IsValid
        {
            get
            {
                return this.particle.IsValid && this.displayUntil >= Game.RawGameTime;
            }
        }

        public float RemainingDuration
        {
            get
            {
                return this.displayUntil - Game.RawGameTime;
            }
        }

        public void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToScreen(this.teleportPosition, 45 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.outline_red", position * 1.12f);
            renderer.DrawTexture(this.mapTexture, position);

            var abilityTexturePosition = position * 0.5f;
            abilityTexturePosition.X += abilityTexturePosition.Width * 0.8f;
            abilityTexturePosition.Y += abilityTexturePosition.Height * 0.6f;

            renderer.DrawTexture("o9k.outline_green_pct100", abilityTexturePosition * 1.2f);
            renderer.DrawTexture(AbilityTexture, abilityTexturePosition);

            position.Y += 50 * Hud.Info.ScreenRatio;

            renderer.DrawText(
                position,
                this.RemainingDuration.ToString("N1"),
                Color.White,
                RendererFontFlags.Center,
                18 * Hud.Info.ScreenRatio);
        }

        public void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
        {
            const float MaxRadius = 22f;
            const float MinRadius = 15f;

            var remainingDuration = this.RemainingDuration;
            var position = minimap.WorldToMinimap(this.teleportPosition, 25 * Hud.Info.ScreenRatio);
            var radius = (((remainingDuration / this.duration) * (MaxRadius - MinRadius)) + MinRadius) * Hud.Info.ScreenRatio;
            var range = new Vector2((float)Math.Cos(-remainingDuration), (float)Math.Sin(-remainingDuration)) * radius;

            renderer.DrawCircle(position.Center, radius, this.color, 3);
            renderer.DrawLine(position.Center, position.Center + range, this.color, 2);
            renderer.DrawTexture(this.minimapTexture, position);
        }
    }
}