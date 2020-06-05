namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Entity;
    using Core.Managers.Particle;
    using Core.Managers.Renderer.Utils;

    using Ensage;
    using Ensage.SDK.Renderer;

    using Helpers.Notificator;

    using MainMenu;

    using SharpDX;

    using Color = System.Drawing.Color;

    [AbilityId(AbilityId.abyssal_underlord_dark_rift)]
    internal class DarkRift : AbilityModule
    {
        private float duration;

        private float endTime;

        private Modifier modifier;

        private Unit9 unit;

        public DarkRift(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            this.EnemyOnly = false;
        }

        protected override void Disable()
        {
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Unit.OnModifierRemoved -= this.OnModifierRemoved;
            this.Context.ParticleManger.ParticleAdded -= this.OnParticleAdded;
            this.Context.Renderer.Draw -= this.OnDraw;
        }

        protected override void Enable()
        {
            Unit.OnModifierAdded += this.OnModifierAdded;
            Unit.OnModifierRemoved += this.OnModifierRemoved;
            this.Context.ParticleManger.ParticleAdded += this.OnParticleAdded;
        }

        private void OnDraw(IRenderer renderer)
        {
            try
            {
                if (this.modifier?.IsValid != true)
                {
                    this.Context.Renderer.Draw -= this.OnDraw;
                    return;
                }

                if (this.unit?.IsVisible != true)
                {
                    return;
                }

                var position = Drawing.WorldToScreen(this.unit.Position);
                if (position.IsZero)
                {
                    return;
                }

                var ratio = Hud.Info.ScreenRatio;
                var remainingTime = this.endTime - Game.RawGameTime;
                var time = Math.Ceiling(remainingTime).ToString("N0");
                var pct = (int)(100 - ((remainingTime / this.duration) * 100));

                var rec = new Rectangle9(position, new Vector2(30 * ratio));
                var outlinePosition = rec * 1.17f;

                renderer.DrawTexture(nameof(AbilityId.abyssal_underlord_dark_rift) + "_rounded", rec);
                renderer.DrawTexture("o9k.modifier_bg", rec);
                renderer.DrawTexture("o9k.outline_green", outlinePosition);
                renderer.DrawTexture("o9k.outline_black" + pct, outlinePosition);
                renderer.DrawText(rec, time, Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, 22 * ratio);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (args.Modifier.Name != "modifier_abyssal_underlord_dark_rift")
                {
                    return;
                }

                this.modifier = args.Modifier;
                this.endTime = this.modifier.DieTime;
                this.duration = this.modifier.Duration;

                this.Context.Renderer.Draw += this.OnDraw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                if (args.Modifier.Name != "modifier_abyssal_underlord_dark_rift")
                {
                    return;
                }

                this.Context.Renderer.Draw -= this.OnDraw;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnParticleAdded(Particle particle)
        {
            try
            {
                if (particle.Released || particle.Name != "particles/units/heroes/heroes_underlord/abbysal_underlord_darkrift_ambient.vpcf")
                {
                    return;
                }

                this.unit = EntityManager9.GetUnit(particle.Sender.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}