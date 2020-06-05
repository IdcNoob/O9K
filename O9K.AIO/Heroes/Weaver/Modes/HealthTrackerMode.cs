namespace O9K.AIO.Heroes.Weaver.Modes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Permanent;

    using Base;

    using Core.Entities.Abilities.Heroes.Weaver;
    using Core.Extensions;
    using Core.Managers.Entity;

    using Ensage;

    using KillStealer;

    using SharpDX;

    internal class HealthTrackerMode : PermanentMode
    {
        private readonly Dictionary<float, float> healthTime = new Dictionary<float, float>();

        private readonly KillSteal killSteal;

        private TimeLapse timeLapse;

        public HealthTrackerMode(BaseHero baseHero, PermanentModeMenu menu)
            : base(baseHero, menu)
        {
            this.Handler.SetUpdateRate(100);
            this.killSteal = baseHero.KillSteal;
        }

        private TimeLapse TimeLapse
        {
            get
            {
                if (this.timeLapse == null)
                {
                    this.timeLapse = EntityManager9.GetAbility<TimeLapse>(this.Owner.Hero);
                }

                return this.timeLapse;
            }
        }

        public override void Disable()
        {
            base.Disable();
            Drawing.OnDraw -= this.OnDraw;
        }

        public override void Dispose()
        {
            base.Disable();
            Drawing.OnDraw -= this.OnDraw;
        }

        public override void Enable()
        {
            base.Enable();
            Drawing.OnDraw += this.OnDraw;
        }

        protected override void Execute()
        {
            var hero = this.Owner.Hero;
            if (hero?.IsValid != true)
            {
                return;
            }

            var time = Game.RawGameTime;

            this.healthTime[time] = hero.Health;

            foreach (var unitPosition in this.healthTime.ToList())
            {
                var key = unitPosition.Key;
                if (key + 6 < time)
                {
                    this.healthTime.Remove(key);
                }
            }
        }

        private void OnDraw(EventArgs args)
        {
            if (this.TimeLapse?.CanBeCasted() != true)
            {
                return;
            }

            var hero = this.Owner.Hero;
            var hpPosition = hero.HealthBarPosition;
            if (hpPosition.IsZero)
            {
                return;
            }

            var time = Game.RawGameTime;
            var health = hero.Health;
            var values = this.healthTime.OrderBy(x => x.Key).ToList();

            var restore = values.Find(x => x.Key + 5f > time).Value;
            if (restore <= health)
            {
                return;
            }

            var restorePercentage = restore / hero.MaximumHealth;
            var healthBarSize = hero.HealthBarSize;
            var start = hpPosition + new Vector2(0, healthBarSize.Y * 0.7f) + this.killSteal.AdditionalOverlayPosition;
            var size = (healthBarSize * new Vector2(restorePercentage, 0.3f)) + this.killSteal.AdditionalOverlayPosition;

#pragma warning disable 618
            Drawing.DrawRect(start, size, Color.DarkOliveGreen);
            Drawing.DrawRect(start - new Vector2(1), size + new Vector2(1), Color.Black, true);

            var restoreEarly = values.Find(x => x.Key + 4f > time).Value;
            if (restoreEarly < restore)
            {
                var losePercentage = (restore - restoreEarly) / hero.MaximumHealth;
                var size2 = (healthBarSize * new Vector2(losePercentage, 0.3f)) + this.killSteal.AdditionalOverlayPosition;
                var start2 = hpPosition + new Vector2(Math.Max(size.X - size2.X, 0), healthBarSize.Y * 0.7f)
                                        + this.killSteal.AdditionalOverlayPosition;

                Drawing.DrawRect(start2, size2, Color.LightGreen);
                Drawing.DrawRect(start2 - new Vector2(1), size2 + new Vector2(1), Color.Black, true);
            }
#pragma warning restore 618
        }
    }
}