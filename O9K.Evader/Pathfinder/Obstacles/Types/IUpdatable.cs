namespace O9K.Evader.Pathfinder.Obstacles.Types
{
    internal interface IUpdatable
    {
        bool IsUpdated { get; }

        void Update();
    }
}