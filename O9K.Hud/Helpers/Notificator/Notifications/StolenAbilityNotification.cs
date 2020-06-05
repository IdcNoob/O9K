namespace O9K.Hud.Helpers.Notificator.Notifications
{
    using Ensage;
    using Ensage.SDK.Renderer;

    using SharpDX;

    internal sealed class StolenAbilityNotification : Notification
    {
        private readonly Ability ability;

        private readonly string abilityName;

        private readonly bool pingOnClick;

        public StolenAbilityNotification(Ability ability, bool ping)
        {
            this.ability = ability;
            this.abilityName = ability.TextureName;
            this.pingOnClick = ping;
            this.TimeToShow = 5;
        }

        public override void Draw(IRenderer renderer, RectangleF position, IMinimap minimap)
        {
            var heroPosition = GetHeroPosition(position);
            var stolenAbilityPosition = GetStolenAbilityPosition(position, heroPosition);
            var abilityPosition = GetAbilityPosition(position, heroPosition, stolenAbilityPosition);
            var opacity = this.GetOpacity();

            renderer.DrawTexture("o9k.notification_bg", position, 0, opacity);
            renderer.DrawTexture(nameof(HeroId.npc_dota_hero_rubick), heroPosition, 0, opacity);
            renderer.DrawTexture(nameof(AbilityId.rubick_spell_steal) + "_rounded", abilityPosition, 0, opacity);
            renderer.DrawTexture(this.abilityName, stolenAbilityPosition, 0, opacity);
        }

        public override bool OnClick()
        {
            if (!this.pingOnClick || !this.ability.IsValid)
            {
                return false;
            }

            this.ability.Announce();
            return true;
        }

        private static RectangleF GetAbilityPosition(RectangleF position, RectangleF heroPosition, RectangleF itemPosition)
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

        private static RectangleF GetStolenAbilityPosition(RectangleF position, RectangleF heroPosition)
        {
            var rec = heroPosition;
            rec.Width *= 0.8f;
            rec.X = position.Right - (position.Width * 0.05f) - rec.Width;

            return rec;
        }
    }
}