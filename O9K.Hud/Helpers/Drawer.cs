namespace O9K.Hud.Helpers
{
    using Ensage.SDK.Renderer;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal static class Drawer
    {
        public static void DrawTextWithBackground(string text, float size, Vector2 position, IRenderer renderer)
        {
            var measureText = renderer.MeasureText(text, size);
            position -= new Vector2(measureText.X / 2, 0);
            var bgPosition = position + new Vector2(0, measureText.Y / 2);

            renderer.DrawLine(
                bgPosition - new Vector2(2, 0),
                bgPosition + new Vector2(measureText.X + 2, 0),
                Color.FromArgb(150, 25, 25, 25),
                measureText.Y);
            renderer.DrawText(position, text, Color.White, size);
        }
    }
}