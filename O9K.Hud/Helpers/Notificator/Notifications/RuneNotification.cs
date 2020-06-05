namespace O9K.Hud.Helpers.Notificator.Notifications
{
    using Core.Helpers;

    using Ensage.SDK.Renderer;

    using SharpDX;

    internal sealed class RuneNotification : Notification
    {
        private readonly bool bounty;

        private readonly Vector3[] positions;

        public RuneNotification(bool bounty, bool playSound, Vector3[] positions)
            : base(playSound)
        {
            this.bounty = bounty;
            this.positions = positions;
            this.TimeToShow = 6;
        }

        public override void Draw(IRenderer renderer, RectangleF position, IMinimap minimap)
        {
            var notificationSize = GetNotificationSize(position);
            var textureSize = GetTextureSize(notificationSize);
            var opacity = this.GetOpacity();

            renderer.DrawTexture("o9k.notification_bg", notificationSize, 0, opacity);
            renderer.DrawTexture(this.bounty ? "o9k.rune_bounty" : "o9k.rune_regen", textureSize, 0, opacity);

            foreach (var vector3 in this.positions)
            {
                renderer.DrawTexture("o9k.ping", minimap.WorldToMinimap(vector3, 25 * Hud.Info.ScreenRatio * this.GetPingSize()));
            }
        }

        private static RectangleF GetNotificationSize(RectangleF position)
        {
            var rec = position;

            rec.X = position.Center.X;
            rec.Width = position.Width * 0.5f;

            return rec;
        }

        private static RectangleF GetTextureSize(RectangleF position)
        {
            var rec = new RectangleF();

            rec.Width = position.Width * 0.5f;
            rec.Height = position.Height;
            rec.X = position.Center.X - (rec.Width / 2f);
            rec.Y = position.Y;

            return rec;
        }
    }
}