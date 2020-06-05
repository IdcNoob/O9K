namespace O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base
{
    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers;

    using SharpDX;

    internal interface IDrawableAbility
    {
        AbilityId AbilityId { get; set; }

        string AbilityTexture { get; }

        bool Draw { get; }

        string HeroTexture { get; }

        bool IsShowingRange { get; }

        bool IsValid { get; }

        Vector3 Position { get; set; }

        void DrawOnMap(IRenderer renderer, IMinimap minimap);

        void DrawOnMinimap(IRenderer renderer, IMinimap minimap);

        void RemoveRange();
    }
}