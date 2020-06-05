namespace O9K.Core.Prediction.Data
{
    using System.Collections.Generic;

    using Entities.Units;

    using SharpDX;

    public class PredictionOutput9
    {
        public List<PredictionOutput9> AoeTargetsHit { get; set; } = new List<PredictionOutput9>();

        public Vector3 BlinkLinePosition { get; set; }

        public Vector3 CastPosition { get; set; }

        public HitChance HitChance { get; set; }

        public Unit9 Target { get; set; }

        public Vector3 TargetPosition { get; set; }
    }
}