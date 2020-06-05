namespace O9K.Hud.Modules.Units.Information
{
    using System;

    using Core.Entities.Heroes;
    using Core.Entities.Units;

    using Ensage.SDK.Renderer;

    using SharpDX;

    using Color = System.Drawing.Color;

    internal class InformationUnit
    {
        public InformationUnit(Unit9 unit)
        {
            this.Unit = unit;
        }

        public int Hits { get; private set; }

        public bool ShouldDraw
        {
            get
            {
                return this.Unit.IsValid && this.Unit.IsVisible && this.Unit.IsAlive;
            }
        }

        public Unit9 Unit { get; }

        public void DrawInformation(IRenderer renderer, float mySpeed, bool showDamage, bool showSpeed, Vector2 position, float size)
        {
            var hpBar = this.Unit.HealthBarPosition;
            if (hpBar.IsZero)
            {
                return;
            }

            var hpSize = this.Unit.HealthBarSize;
            var iconSize = size * 0.75f;

            var iconPosition = hpBar + position + new Vector2(hpSize.X, size - iconSize);
            var textPosition = hpBar + position + new Vector2(hpSize.X + iconSize + 3, 0);

            if (showDamage)
            {
                renderer.DrawTexture("o9k.attack_minimalistic", iconPosition, new Vector2(iconSize));
                renderer.DrawText(textPosition, this.Hits == 0 ? "?" : this.Hits.ToString(), Color.White, size);

                iconPosition += new Vector2(0, size);
                textPosition += new Vector2(0, size);
            }

            if (showSpeed)
            {
                renderer.DrawTexture("o9k.speed_minimalistic", iconPosition, new Vector2(iconSize));

                var speed = mySpeed - this.Unit.Speed;
                if (speed >= 0)
                {
                    renderer.DrawText(textPosition, speed.ToString(), Color.White, size);
                }
                else
                {
                    renderer.DrawText(textPosition, (speed * -1).ToString(), Color.DarkOrange, size);
                }
            }
        }

        public void UpdateDamage(Hero9 myHero)
        {
            this.Hits = 0;

            if (!this.Unit.IsAlive)
            {
                return;
            }

            var count = (int)Math.Ceiling(
                this.Unit.Health / (myHero.GetAttackDamage(this.Unit) - (this.Unit.HealthRegeneration * myHero.SecondsPerAttack)));

            if (count > 0)
            {
                this.Hits = count;
            }
        }
    }
}