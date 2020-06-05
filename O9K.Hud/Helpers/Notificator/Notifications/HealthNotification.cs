namespace O9K.Hud.Helpers.Notificator.Notifications
{
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage.SDK.Renderer;

    using SharpDX;

    internal sealed class HealthNotification : Notification
    {
        private readonly bool moveCamera;

        private readonly Unit9 unit;

        public HealthNotification(Unit9 unit, bool moveCamera)
        {
            this.unit = unit;
            this.moveCamera = moveCamera;
            this.TimeToShow = 6;
        }

        public override void Draw(IRenderer renderer, RectangleF position, IMinimap minimap)
        {
            var notificationSize = GetNotificationSize(position);
            var textureSize = GetTextureSize(notificationSize);
            var healthSize = GetHealthSize(textureSize);
            var manaSize = GetManaSize(textureSize);
            var opacity = this.GetOpacity();

            renderer.DrawTexture("o9k.notification_bg", notificationSize, 0, opacity);
            renderer.DrawTexture(this.unit.Name, textureSize, 0, opacity);

            renderer.DrawTexture("o9k.health_enemy_bg", healthSize, 0, opacity);
            renderer.DrawTexture("o9k.mana_bg", manaSize, 0, opacity);

            if (!this.unit.IsAlive)
            {
                return;
            }

            renderer.DrawTexture("o9k.health_enemy", this.GetCurrentHealthSize(healthSize), 0, opacity);
            renderer.DrawTexture("o9k.mana", this.GetCurrentManaSize(manaSize), 0, opacity);
        }

        public override bool OnClick()
        {
            if (!this.moveCamera)
            {
                return false;
            }

            Hud.CameraPosition = this.unit.Position;
            return true;
        }

        private static RectangleF GetHealthSize(RectangleF position)
        {
            var rec = position;

            rec.Y += rec.Height * 0.85f;
            rec.Height *= 0.15f;

            return rec;
        }

        private static RectangleF GetManaSize(RectangleF position)
        {
            var rec = position;

            rec.Y += rec.Height;
            rec.Height *= 0.15f;

            return rec;
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
            var rec = position;

            rec.X += position.Width * 0.15f;
            rec.Y += position.Height * 0.12f;
            rec.Width = position.Width * 0.7f;
            rec.Height = position.Height * 0.7f;

            return rec;
        }

        private RectangleF GetCurrentHealthSize(RectangleF position)
        {
            var rec = position;

            rec.Width *= this.unit.HealthPercentageBase;

            return rec;
        }

        private RectangleF GetCurrentManaSize(RectangleF position)
        {
            var rec = position;

            rec.Width *= this.unit.ManaPercentageBase;

            return rec;
        }
    }
}