namespace O9K.Evader.Metadata
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Units;

    using Pathfinder.Obstacles;

    using SharpDX;

    internal interface IPathfinder
    {
        event EventHandler<IObstacle> AbilityCanceled;

        event EventHandler<bool> ObstacleAdded;

        List<uint> AddNavMeshObstacle(Vector3 start, Vector3 end, float radius);

        Dictionary<uint, Vector3> AddNavMeshObstacle(Vector3 startPosition, Vector3 endPosition, float radius, float endRadius);

        List<uint> AddNavMeshObstacle(Vector3 position, float radius);

        void AddObstacle(IObstacle obstacle);

        void CancelObstacle(uint abilityHandle, bool forceCancel = false, bool first = false);

        bool CanEvadeObstacle(Unit9 unit, Vector3 position, float remainingTime);

        IEnumerable<IObstacle> GetIntersectingObstacles(Unit9 unit);

        IEnumerable<Vector3> GetPathFromObstacle(Unit9 unit, float speed, Vector3 position, float remainingTime, out bool success);

        void RemoveNavMeshObstacle(IEnumerable<uint> obstacleIds);

        void RemoveNavMeshObstacle(Dictionary<uint, Vector3> obstacleIds);
    }
}