namespace O9K.Hud.Modules.Map.Predictions.LaneCreeps
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core.Data;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;
    using Ensage.SDK.Geometry;

    using LaneData;

    using SharpDX;

    internal class CreepWave
    {
        public List<Unit9> Creeps = new List<Unit9>();

        private readonly Vector3 endPosition;

        private readonly int pathLength;

        private int lastPoint;

        private Vector3 lastVisiblePosition;

        private Vector3 predictedPosition;

        private Vector3[] remainingPath;

        public CreepWave(LanePosition lane, Vector3[] path)
        {
            this.Lane = lane;
            this.Path = path;
            this.pathLength = this.Path.Length;
            this.endPosition = path[this.pathLength - 1];
            this.predictedPosition = path[0];
        }

        public bool IsSpawned { get; private set; }

        public bool IsValid
        {
            get
            {
                return this.Creeps.Any(x => x.IsValid && x.BaseUnit.IsAlive) && this.PredictedPosition.Distance2D(this.endPosition) > 300;
            }
        }

        public bool IsVisible
        {
            get
            {
                return this.Creeps.Any(x => x.IsValid && x.IsVisible);
            }
        }

        public LanePosition Lane { get; }

        public float LastVisibleTime { get; private set; }

        public Vector3[] Path { get; }

        public Vector3 Position
        {
            get
            {
                var creeps = this.Creeps.Where(x => x.IsValid && x.IsVisible).ToList();

                //if (creeps.Count(x => x.IsVisible) <= Creeps.Count / 2)
                //{
                //    return lastVisiblePosition;
                //}

                this.lastVisiblePosition = creeps.Aggregate(new Vector3(), (position, creep) => position + creep.Position) / creeps.Count;
                this.LastVisibleTime = Game.RawGameTime;
                this.remainingPath = null;

                return this.lastVisiblePosition;
            }
        }

        public Vector3 PredictedPosition
        {
            get
            {
                return this.predictedPosition;
            }
            set
            {
                this.predictedPosition = value;

                if (this.predictedPosition.Distance2D(this.Path[this.lastPoint]) < 500)
                {
                    this.lastPoint = Math.Min(this.lastPoint + 1, this.pathLength - 1);
                }
            }
        }

        public Vector3[] RemainingPath
        {
            get
            {
                if (this.remainingPath != null)
                {
                    return this.remainingPath;
                }

                var remainingPoints = this.pathLength - this.lastPoint;

                this.remainingPath = new Vector3[remainingPoints + 1];
                this.remainingPath[0] = this.lastVisiblePosition;
                Array.Copy(this.Path, this.lastPoint, this.remainingPath, 1, remainingPoints);

                return this.remainingPath;
            }
        }

        public float SpawnTime { get; private set; }

        public bool WasVisible
        {
            get
            {
                return this.Creeps.Any(x => x.IsValid && x.BaseUnit.IsSpawned);
            }
        }

        public void Spawn()
        {
            this.IsSpawned = true;
            this.SpawnTime = Game.RawGameTime + 0.4f;
        }

        public void Update()
        {
            if (!this.WasVisible)
            {
                this.PredictedPosition = this.Path.PositionAfter(Game.RawGameTime - this.SpawnTime, GameData.CreepSpeed);
            }
            else if (this.IsVisible)
            {
                this.PredictedPosition = this.Position;
            }
            else
            {
                if (this.LastVisibleTime <= 0)
                {
                    this.LastVisibleTime = Game.RawGameTime;
                }

                this.PredictedPosition = this.RemainingPath.PositionAfter(Game.RawGameTime - this.LastVisibleTime, GameData.CreepSpeed);
            }
        }
    }
}