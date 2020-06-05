namespace O9K.Core.Helpers
{
    using System.Collections.Generic;

    using Ensage;

    using SharpDX;

    public static class Drawer
    {
        private static readonly List<ParticleEffect> Particles = new List<ParticleEffect>();

        private static ParticleEffect greenParticle;

        private static ParticleEffect redParticle;

        public static void AddGreenCircle(Vector3 position)
        {
            var green = new ParticleEffect(@"materials\ensage_ui\particles\drag_selected_ring_mod.vpcf", position);
            green.SetControlPoint(1, new Vector3(0, 255, 0));
            green.SetControlPoint(2, new Vector3(50, 255, 0));

            Particles.Add(green);
        }

        public static void AddRedCircle(Vector3 position)
        {
            var red = new ParticleEffect(@"materials\ensage_ui\particles\drag_selected_ring_mod.vpcf", position);
            red.SetControlPoint(1, new Vector3(255, 0, 0));
            red.SetControlPoint(2, new Vector3(70, 255, 0));

            Particles.Add(red);
        }

        public static void Dispose()
        {
            foreach (var particleEffect in Particles)
            {
                particleEffect?.Dispose();
            }

            greenParticle?.Dispose();
            redParticle?.Dispose();
        }

        public static void DrawGreenCircle(Vector3 position)
        {
            if (greenParticle == null)
            {
                greenParticle = new ParticleEffect(@"materials\ensage_ui\particles\drag_selected_ring_mod.vpcf", position);
                greenParticle.SetControlPoint(1, new Vector3(0, 255, 0));
                greenParticle.SetControlPoint(2, new Vector3(50, 255, 0));
            }

            greenParticle.SetControlPoint(0, position);
        }

        public static void DrawRedCircle(Vector3 position)
        {
            if (redParticle == null)
            {
                redParticle = new ParticleEffect(@"materials\ensage_ui\particles\drag_selected_ring_mod.vpcf", position);
                redParticle.SetControlPoint(1, new Vector3(255, 0, 0));
                redParticle.SetControlPoint(2, new Vector3(70, 255, 0));
            }

            redParticle.SetControlPoint(0, position);
        }
    }
}