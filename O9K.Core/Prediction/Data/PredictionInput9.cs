namespace O9K.Core.Prediction.Data
{
    using System.Collections.Generic;

    using Collision;

    using Entities.Units;

    public class PredictionInput9
    {
        public bool AreaOfEffect { get; set; }

        public IReadOnlyList<Unit9> AreaOfEffectTargets { get; set; } = new List<Unit9>();

        public Unit9 Caster { get; set; }

        public float CastRange { get; set; }

        public CollisionTypes CollisionTypes { get; set; }

        public float Delay { get; set; }

        public float EndRadius { get; set; }

        public float Radius { get; set; }

        public float Range { get; set; }

        public bool RequiresToTurn { get; set; }

        public SkillShotType SkillShotType { get; set; }

        public float Speed { get; set; }

        public Unit9 Target { get; set; }

        public bool UseBlink { get; set; }
    }
}