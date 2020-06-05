namespace O9K.Core.Logger.Data
{
    using System;

    using Core.Data;

    using Ensage;

    [Serializable]
    internal sealed class GameLogData
    {
        public GameLogData()
        {
            try
            {
                var hero = ObjectManager.LocalHero;

                this.Hero = hero.Name;
                this.Team = hero.Team.ToString();
                this.Map = Game.ShortLevelName;
                this.Mode = Game.GameMode.ToString();
                this.State = Game.GameState.ToString();
                this.Time = GameData.DisplayTime;
            }
            catch
            {
                // ignored
            }
        }

        public string Hero { get; }

        public string Map { get; }

        public string Mode { get; }

        public string State { get; }

        public string Team { get; }

        public string Time { get; }
    }
}