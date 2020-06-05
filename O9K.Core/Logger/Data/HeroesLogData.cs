namespace O9K.Core.Logger.Data
{
    using System;
    using System.Linq;

    using Ensage;

    [Serializable]
    internal sealed class HeroesLogData
    {
        public HeroesLogData()
        {
            try
            {
                var allyTeam = ObjectManager.LocalPlayer.Team;
                var players = ObjectManager.GetEntities<Player>()
                    .Where(x => x?.IsValid == true && (x.Team == Team.Dire || x.Team == Team.Radiant))
                    .ToList();

                this.AllyHeroes = players.Where(x => x.Team == allyTeam)
                    .OrderBy(x => x.Id)
                    .Select(x => "[" + x.Id + "] " + x.SelectedHeroId)
                    .ToArray();
                this.EnemyHeroes = players.Where(x => x.Team != allyTeam)
                    .OrderBy(x => x.Id)
                    .Select(x => "[" + x.Id + "] " + x.SelectedHeroId)
                    .ToArray();
            }
            catch
            {
                this.AllyHeroes = new[] { "Unknown" };
                this.EnemyHeroes = new[] { "Unknown" };
            }
        }

        public string[] AllyHeroes { get; }

        public string[] EnemyHeroes { get; }
    }
}