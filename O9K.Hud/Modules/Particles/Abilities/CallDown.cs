namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;

    using Core.Entities.Metadata;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Particle;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    using SharpDX;

    [AbilityId(AbilityId.gyrocopter_call_down)]
    internal class CallDown : AbilityModule
    {
        private readonly Vector3 radius;

        public CallDown(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            var radiusData = new SpecialData(AbilityId.gyrocopter_call_down, "radius").GetValue(1);
            this.radius = new Vector3(radiusData, -radiusData, -radiusData);
        }

        protected override void Disable()
        {
            this.Context.ParticleManger.ParticleAdded -= this.OnParticleAdded;
        }

        protected override void Enable()
        {
            this.Context.ParticleManger.ParticleAdded += this.OnParticleAdded;
        }

        private void OnParticleAdded(Particle particle)
        {
            try
            {
                if (!particle.Released || particle.Name != "particles/units/heroes/hero_gyrocopter/gyro_calldown_first.vpcf")
                {
                    return;
                }

                var effect = new ParticleEffect(
                    "particles/units/heroes/hero_gyrocopter/gyro_calldown_marker.vpcf",
                    particle.GetControlPoint(1));
                effect.SetControlPoint(1, this.radius);

                effect.Release();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}