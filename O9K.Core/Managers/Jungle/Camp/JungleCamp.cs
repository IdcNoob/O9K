namespace O9K.Core.Managers.Jungle.Camp
{
    using Ensage;

    using SharpDX;

    internal class JungleCamp : IJungleCamp
    {
        public Vector3 CreepsPosition { get; set; }

        public Vector3 DrawPosition { get; set; }

        public uint Id { get; set; }

        public bool IsAncient { get; set; }

        public bool IsLarge { get; set; }

        public bool IsMedium { get; set; }

        public bool IsSmall { get; set; }

        public float PullTime { get; set; }

        public float StackTime { get; set; }

        public Team Team { get; set; }
    }
}