namespace O9K.Hud.Modules.Particles.Abilities
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Metadata;
    using Core.Helpers;
    using Core.Logger;
    using Core.Managers.Context;
    using Core.Managers.Particle;

    using Ensage;

    using Helpers.Notificator;

    using MainMenu;

    using SharpDX;

    [AbilityId(AbilityId.snapfire_mortimer_kisses)]
    internal class MortimerKisses : AbilityModule
    {
        private readonly Queue<ParticleEffect> effects = new Queue<ParticleEffect>();

        private readonly Vector3 radius;

        public MortimerKisses(IContext9 context, INotificator notificator, IHudMenu hudMenu)
            : base(context, notificator, hudMenu)
        {
            var radiusData = new SpecialData(AbilityId.snapfire_mortimer_kisses, "impact_radius").GetValue(1);
            this.radius = new Vector3(radiusData, -radiusData, -radiusData);
        }

        protected override void Disable()
        {
            this.Context.ParticleManger.ParticleAdded -= this.ParticleMangerOnParticleAdded;
        }

        protected override void Enable()
        {
            this.Context.ParticleManger.ParticleAdded += this.ParticleMangerOnParticleAdded;
        }

        private void ParticleMangerOnParticleAdded(Particle particle)
        {
            try
            {
                if (particle.Released)
                {
                    return;
                }

                switch (particle.Name)
                {
                    case "particles/units/heroes/hero_snapfire/hero_snapfire_ultimate_linger.vpcf":
                    {
                        if (this.effects.Count == 0)
                        {
                            return;
                        }

                        var effect = this.effects.Dequeue();
                        if (effect.IsValid)
                        {
                            effect.Dispose();
                        }

                        break;
                    }
                    case "particles/units/heroes/hero_snapfire/snapfire_lizard_blobs_arced.vpcf":
                    {
                        var effect = new ParticleEffect(
                            "particles/units/heroes/hero_snapfire/hero_snapfire_ultimate_calldown.vpcf",
                            particle.GetControlPoint(1));
                        effect.SetControlPoint(1, this.radius);
                        effect.SetControlPoint(2, new Vector3(0.8f, 0, 0));

                        this.effects.Enqueue(effect);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}