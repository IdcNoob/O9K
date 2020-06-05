namespace O9K.Core.Managers.Jungle.Camp
{
    using Ensage;

    using SharpDX;

    public interface IJungleCamp
    {
        Vector3 CreepsPosition { get; }

        Vector3 DrawPosition { get; }

        uint Id { get; }

        bool IsAncient { get; }

        bool IsLarge { get; }

        bool IsMedium { get; }

        bool IsSmall { get; }

        float PullTime { get; }

        float StackTime { get; }

        Team Team { get; }
    }
}