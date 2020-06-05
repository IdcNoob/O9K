namespace O9K.Hud.Helpers.Notificator.Notifications
{
    using Core.Helpers;

    using Ensage.SDK.Renderer;

    using SharpDX;

    internal class PingNotification : Notification
    {
        private readonly Vector3 pingPosition;

        public PingNotification(Vector3 position, bool playSound)
            : base(playSound)
        {
            this.pingPosition = position;
            this.TimeToShow = 5;
        }

        public override void Draw(IRenderer renderer, RectangleF position, IMinimap minimap)
        {
            renderer.DrawTexture("o9k.ping", minimap.WorldToMinimap(this.pingPosition, 25 * Hud.Info.ScreenRatio * this.GetPingSize()));
        }
    }
}