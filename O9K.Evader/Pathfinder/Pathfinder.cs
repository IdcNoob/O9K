namespace O9K.Evader.Pathfinder
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Extensions;
    using Ensage.SDK.Geometry;
    using Ensage.SDK.Handlers;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Obstacles;
    using Obstacles.Types;

    using SharpDX;

    [Export(typeof(IPathfinder))]
    internal class Pathfinder : IPathfinder, IEvaderService
    {
        public enum EvadeMode
        {
            All,

            Disables,

            None
        }

        private readonly Dictionary<uint, List<uint>> navMeshObstacles = new Dictionary<uint, List<uint>>();

        private readonly List<IObstacle> obstacles = new List<IObstacle>();

        private uint obstacleAutoId;

        private IUpdateHandler updateHandler;

        public event EventHandler<IObstacle> AbilityCanceled;

        public event EventHandler<bool> ObstacleAdded;

        public LoadOrder LoadOrder { get; } = LoadOrder.Pathfinder;

        public NavMeshPathfinding NavMesh { get; private set; }

        public IEnumerable<IObstacle> Obstacles
        {
            get
            {
                return this.obstacles;
            }
        }

        public void Activate()
        {
            this.NavMesh = new NavMeshPathfinding();
            this.updateHandler = UpdateManager.Subscribe(this.OnUpdate, 0, false);
            EntityManager9.UnitMonitor.UnitDied += this.OnUnitDied;

            foreach (var building in EntityManager9.Units.Where(x => x.IsBuilding && x.IsAlive))
            {
                this.navMeshObstacles.Add(building.Handle, this.AddNavMeshObstacle(building.Position, building.HullRadius * 0.75f));
            }
        }

        public List<uint> AddNavMeshObstacle(Vector3 start, Vector3 end, float radius)
        {
            //var max = Math.Min(start.Distance2D(end) - 25, 100);

            var lineObstacles = new List<uint>
            {
                this.NavMesh.AddObstacle(start, end, radius),
                //this.NavMesh.AddObstacle(start.Extend2D(end, max), end, radius),
                //this.NavMesh.AddObstacle(start, end.Extend2D(start, max), radius)
            };

            var circleDistance = (int)(radius / 2);
            var from = (int)(radius / circleDistance);
            var to = (int)((start.Distance2D(end) - radius) / circleDistance);

            for (var i = from; i < to; i++)
            {
                var position = start.Extend2D(end, circleDistance * i);
                lineObstacles.Add(this.NavMesh.AddObstacle(position, radius - 50));
            }

            return lineObstacles;
        }

        public Dictionary<uint, Vector3> AddNavMeshObstacle(Vector3 startPosition, Vector3 endPosition, float radius, float endRadius)
        {
            var coneObstacles = new Dictionary<uint, Vector3>();
            var lineCount = (int)Math.Max(Math.Ceiling(endRadius / radius), 3);
            var degree = endRadius / 25f;
            var diffAngle = (degree * 2) / (lineCount - 1);
            var diffPosition = endPosition - startPosition;
            var range = startPosition.Distance2D(endPosition);

            for (var i = 0; i < lineCount; i++)
            {
                var angle = degree - (i * diffAngle);
                var rotation = diffPosition.Rotated(MathUtil.DegreesToRadians(angle)) * radius;
                var end = startPosition.Extend2D(rotation, range);
                //  var max = Math.Min(startPosition.Distance2D(end) - 25, 100);

                coneObstacles.Add(this.NavMesh.AddObstacle(startPosition, end, radius), end);

                var circleDistance = (int)(radius / 2);
                var from = (int)(radius / circleDistance);
                var to = (int)((startPosition.Distance2D(end) - radius) / circleDistance);

                for (var j = from; j < to; j++)
                {
                    var position = startPosition.Extend2D(end, circleDistance * j);
                    coneObstacles.Add(this.NavMesh.AddObstacle(position, radius - 50), end);
                }

                //coneObstacles.Add(this.NavMesh.AddObstacle(startPosition.Extend2D(end, max), end, radius), end);
                //coneObstacles.Add(this.NavMesh.AddObstacle(startPosition, end.Extend2D(startPosition, max), radius), end);
            }

            return coneObstacles;
        }

        public List<uint> AddNavMeshObstacle(Vector3 position, float radius)
        {
            var circleObstacles = new List<uint>();
            var lineCount = (int)Math.Ceiling(radius / 70);
            var lineRadius = radius / lineCount;

            for (var i = 0; i < lineCount; i++)
            {
                var alpha = (Math.PI / lineCount) * i;
                var polar = new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);
                var range = polar * radius;
                var start = position - range;
                var end = position + range;

                var max = Math.Min(start.Distance2D(end) - 25, 150);

                circleObstacles.Add(this.NavMesh.AddObstacle(start, end, lineRadius));
                circleObstacles.Add(this.NavMesh.AddObstacle(start.Extend2D(end, max), end, lineRadius));
                circleObstacles.Add(this.NavMesh.AddObstacle(start, end.Extend2D(start, max), lineRadius));
            }

            return circleObstacles;
        }

        public void AddObstacle(IObstacle obstacle)
        {
            if (obstacle.Id == 0)
            {
                obstacle.Id = ++this.obstacleAutoId;
            }

            this.obstacles.Add(obstacle);

            this.updateHandler.IsEnabled = true;
            this.ObstacleAdded?.Invoke(this, true);
        }

        public void CancelObstacle(uint abilityHandle, bool forceCancel = false, bool first = false)
        {
            var obstacle = first
                               ? this.obstacles.Find(x => x.EvadableAbility.Ability.Handle == abilityHandle && !x.IsModifierObstacle)
                               : this.obstacles.LastOrDefault(
                                   x => x.EvadableAbility.Ability.Handle == abilityHandle && !x.IsModifierObstacle);

            if (obstacle == null)
            {
                return;
            }

            this.RemoveObstacle(obstacle);

            if (!forceCancel)
            {
                this.AbilityCanceled?.Invoke(this, obstacle);
            }
        }

        public bool CanEvadeObstacle(Unit9 unit, Vector3 position, float remainingTime)
        {
            this.GetPathFromObstacle(unit, unit.Speed, position, remainingTime, out var success);
            return success;
        }

        public void Dispose()
        {
            UpdateManager.Unsubscribe(this.updateHandler);
            EntityManager9.UnitMonitor.UnitDied -= this.OnUnitDied;

            this.NavMesh.Dispose();
            this.obstacles.Clear();
            this.navMeshObstacles.Clear();
        }

        public IEnumerable<IObstacle> GetIntersectingObstacles(Unit9 unit)
        {
            return this.obstacles.Where(x => x.EvadableAbility.Owner.IsValid && x.IsIntersecting(unit, true));
        }

        public IEnumerable<Vector3> GetPathFromObstacle(Unit9 unit, float speed, Vector3 position, float remainingTime, out bool success)
        {
            return this.NavMesh.CalculatePathFromObstacle(
                position,
                unit.Position,
                unit.BaseUnit.NetworkRotationRad,
                speed,
                unit.TurnRate,
                remainingTime * 1000,
                true,
                out success);
        }

        public void RemoveNavMeshObstacle(IEnumerable<uint> obstacleIds)
        {
            foreach (var obstacleId in obstacleIds)
            {
                this.NavMesh.RemoveObstacle(obstacleId);
            }
        }

        public void RemoveNavMeshObstacle(Dictionary<uint, Vector3> obstacleIds)
        {
            this.RemoveNavMeshObstacle(obstacleIds.Select(x => x.Key));
        }

        private void OnUnitDied(Unit9 entity)
        {
            try
            {
                if (!this.navMeshObstacles.TryGetValue(entity.Handle, out var obstacleIds))
                {
                    return;
                }

                this.RemoveNavMeshObstacle(obstacleIds);
                this.navMeshObstacles.Remove(entity.Handle);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnUpdate()
        {
            try
            {
                foreach (var obstacle in this.obstacles.ToList())
                {
                    try
                    {
                        switch (obstacle)
                        {
                            case IExpirable expirable when expirable.IsExpired:
                            {
                                this.RemoveObstacle(obstacle);
                                return; // todo change to break and delete debug code
                            }
                            case IUpdatable updatable when !updatable.IsUpdated:
                            {
                                updatable.Update();
                                break;
                            }
                        }

                        //if (obstacle is AbilityObstacle ability)
                        //{
                        //    if (float.IsNaN(ability.EndObstacleTime) || float.IsInfinity(ability.EndObstacleTime))
                        //    {
                        //        var e = new BrokenAbilityException(ability.EvadableAbility.Ability.Name);

                        //        try
                        //        {
                        //            e.Data["Ability"] = new
                        //            {
                        //                Ability = ability.EvadableAbility.Ability.Name,
                        //                Owner = ability.EvadableAbility.Ability.Owner.Name,
                        //                ability.EndCastTime,
                        //                ability.EndObstacleTime
                        //            };
                        //        }
                        //        catch
                        //        {
                        //            e.Data["Ability"] = "Unknown";
                        //        }

                        //        throw e;
                        //    }
                        //}
                    }
                    catch // (Exception e)
                    {
                        //todo fix broken abilities ...
                        //Logger.Error(e);
                        this.RemoveObstacle(obstacle);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void RemoveObstacle(IObstacle obstacle)
        {
            this.obstacles.Remove(obstacle);

            if (obstacle is IDisposable disposable)
            {
                disposable.Dispose();
            }

            if (this.obstacles.Count == 0)
            {
                this.updateHandler.IsEnabled = false;
                this.ObstacleAdded?.Invoke(this, false);
            }
        }
    }
}