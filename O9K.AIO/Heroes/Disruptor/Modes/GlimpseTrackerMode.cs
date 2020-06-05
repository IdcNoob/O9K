namespace O9K.AIO.Heroes.Disruptor.Modes
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Permanent;

    using Base;

    using Core.Entities.Units;

    using Ensage;

    using SharpDX;

    internal class GlimpseTrackerMode : PermanentMode
    {
        private readonly Vector3 color = new Vector3(Color.Blue.R, Color.Blue.G, Color.Blue.B);

        private readonly Dictionary<Unit9, Dictionary<float, Vector3>> positions = new Dictionary<Unit9, Dictionary<float, Vector3>>();

        private ParticleEffect targetParticleEffect;

        public GlimpseTrackerMode(BaseHero baseHero, PermanentModeMenu menu)
            : base(baseHero, menu)
        {
        }

        protected override void Execute()
        {
            var time = Game.RawGameTime;

            foreach (var unit in this.TargetManager.EnemyHeroes)
            {
                if (!this.positions.TryGetValue(unit, out var unitPositions))
                {
                    unitPositions = new Dictionary<float, Vector3>();
                    this.positions[unit] = unitPositions;
                }
                else
                {
                    foreach (var unitPosition in unitPositions.ToList())
                    {
                        var key = unitPosition.Key;
                        if (key + 4.1f < time)
                        {
                            unitPositions.Remove(key);
                        }
                    }
                }

                unitPositions[time] = unit.Position;
            }

            if (this.TargetManager.HasValidTarget)
            {
                if (!this.positions.TryGetValue(this.TargetManager.Target, out var unitPositions))
                {
                    return;
                }

                var glimpsePosition = unitPositions.OrderBy(x => x.Key).FirstOrDefault(x => time - x.Key > 4f).Value;
                if (glimpsePosition.IsZero)
                {
                    this.RemoveGlimpseParticle();
                    return;
                }

                this.DrawGlimpseParticle(glimpsePosition);
            }
            else
            {
                this.RemoveGlimpseParticle();
            }
        }

        private void DrawGlimpseParticle(Vector3 position)
        {
            if (this.targetParticleEffect == null)
            {
                this.targetParticleEffect = new ParticleEffect(@"materials\ensage_ui\particles\target.vpcf", position);
                this.targetParticleEffect.SetControlPoint(6, new Vector3(255));
            }

            this.targetParticleEffect.SetControlPoint(2, this.TargetManager.Target.Position);
            this.targetParticleEffect.SetControlPoint(5, this.color);
            this.targetParticleEffect.SetControlPoint(7, position);
        }

        private void RemoveGlimpseParticle()
        {
            if (this.targetParticleEffect == null)
            {
                return;
            }

            this.targetParticleEffect.Dispose();
            this.targetParticleEffect = null;
        }
    }
}