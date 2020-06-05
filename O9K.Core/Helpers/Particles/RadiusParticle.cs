namespace O9K.Core.Helpers.Particles
{
    using System;

    using Ensage;

    using Entities.Units;

    using SharpDX;

    public class RadiusParticle : IDisposable
    {
        private const string ParticleName = "particles/ui_mouseactions/drag_selected_ring.vpcf";

        private readonly ParticleEffect particleEffect;

        public RadiusParticle(Vector3 position, Vector3 color, float radius)
        {
            this.particleEffect = new ParticleEffect(ParticleName, position);
            this.SetData(color, radius);
        }

        public RadiusParticle(Unit9 unit, Vector3 color, float radius)
        {
            this.particleEffect = new ParticleEffect(ParticleName, unit.BaseUnit);
            this.SetData(color, radius);
        }

        public void ChangePosition(Vector3 position)
        {
            this.particleEffect.SetControlPoint(0, position);
            this.particleEffect.FullRestart();
        }

        public void Dispose()
        {
            this.particleEffect.Dispose();
        }

        private void SetData(Vector3 color, float radius)
        {
            this.particleEffect.SetControlPoint(1, color);
            this.particleEffect.SetControlPoint(2, new Vector3(-radius, 255, 0));
        }
    }
}