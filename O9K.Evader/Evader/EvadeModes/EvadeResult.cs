namespace O9K.Evader.Evader.EvadeModes
{
    using System;

    internal class EvadeResult : IEquatable<EvadeResult>
    {
        public enum EvadeState
        {
            Failed,

            TooEarly,

            Ignore,

            Successful
        }

        public string AbilityOwner { get; set; }

        public string Ally { get; set; }

        public string AllyAbility { get; set; }

        public string Enemy { get; set; }

        public string EnemyAbility { get; set; }

        public bool IsModifier { get; set; }

        public EvadeMode Mode { get; set; }

        public uint ObstacleId { get; set; }

        public EvadeState State { get; set; }

        public bool Equals(EvadeResult other)
        {
            if (other == null)
            {
                return false;
            }

            return this.Ally == other.Ally && this.ObstacleId == other.ObstacleId && this.State == other.State;
        }
    }
}