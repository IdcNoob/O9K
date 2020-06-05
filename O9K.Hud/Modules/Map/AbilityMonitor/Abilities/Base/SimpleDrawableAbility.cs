namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
    using Core.Helpers;

    using Ensage.SDK.Renderer;

    using Helpers;

    internal class SimpleDrawableAbility : DrawableAbility
    {
        public override void DrawOnMap(IRenderer renderer, IMinimap minimap)
        {
            var position = minimap.WorldToScreen(this.Position, 35 * Hud.Info.ScreenRatio);
            if (position.IsZero)
            {
                return;
            }

            renderer.DrawTexture("o9k.outline_red", position * 1.12f);
            renderer.DrawTexture(this.AbilityTexture, position);
        }

        public override void DrawOnMinimap(IRenderer renderer, IMinimap minimap)
        {
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