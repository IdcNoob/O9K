namespace O9K.Hud.Helpers.Notificator.Notifications
{
    using Core.Entities.Abilities.Base;

    using Ensage;
    using Ensage.SDK.Renderer;

    using SharpDX;

    internal sealed class PurchaseNotification : Notification
    {
        private readonly string heroName;

        private readonly Item item;

        private readonly string itemName;

        private readonly bool pingOnClick;

        public PurchaseNotification(Ability9 ability, bool ping)
        {
            this.pingOnClick = ping;
            this.heroName = ability.Owner.TextureName;
            this.itemName = ability.TextureName;
            this.item = ability.BaseItem;
        }

        public override void Draw(IRenderer renderer, RectangleF position, IMinimap minimap)
        {
            var heroPosition = GetHeroPosition(position);
            var itemPosition = GetItemPosition(position, heroPosition);
            var goldPosition = GetGoldPosition(position, heroPosition, itemPosition);
            var opacity = this.GetOpacity();

            renderer.DrawTexture("o9k.notification_bg", position, 0, opacity);
            renderer.DrawTexture(this.heroName, heroPosition, 0, opacity);
            renderer.DrawTexture("o9k.gold", goldPosition, 0, opacity);
            renderer.DrawTexture(this.itemName, itemPosition, 0, opacity);
        }

        public override bool OnClick()
        {
            if (!this.pingOnClick || !this.item.IsValid)
            {
                return false;
            }

            this.item.Announce();
            return true;
        }

        private static RectangleF GetGoldPosition(RectangleF position, RectangleF heroPosition, RectangleF itemPosition)
        {
            var rec = new RectangleF();

            rec.Width = position.Width * 0.18f;
            rec.Height = position.Height * 0.6f;
            rec.X = ((heroPosition.Right + itemPosition.Left) / 2f) - (rec.Width / 2f);
            rec.Y = (position.Y + (position.Height / 2f)) - (rec.Height / 2);

            return rec;
        }

        private static RectangleF GetHeroPosition(RectangleF position)
        {
            var rec = position;

            rec.X += position.Width * 0.05f;
            rec.Y += position.Height * 0.15f;
            rec.Width = position.Width * 0.3f;
            rec.Height = position.Height * 0.7f;

            return rec;
        }

        private static RectangleF GetItemPosition(RectangleF position, RectangleF heroPosition)
        {
            var rec = heroPosition;
            rec.Width *= 0.8f;
            rec.X = position.Right - (position.Width * 0.05f) - rec.Width;

            return rec;
        }
    }
}