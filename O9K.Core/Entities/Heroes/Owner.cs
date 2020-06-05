namespace O9K.Core.Entities.Heroes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Logger;

    using Managers.Entity;

    using Units;

    public sealed class Owner
    {
        public Owner()
        {
            try
            {
                this.Player = ObjectManager.LocalPlayer;
                this.PlayerHandle = this.Player.Handle;
                this.Team = this.Player.Team;
                this.EnemyTeam = this.Team == Team.Radiant ? Team.Dire : Team.Radiant;
                this.HeroId = this.Player.SelectedHeroId;
                this.HeroName = this.HeroId.ToString();
                this.HeroDisplayName = LocalizationHelper.LocalizeName(this.HeroName);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public Team EnemyTeam { get; }

        public Hero9 Hero { get; private set; }

        public string HeroDisplayName { get; }

        public uint HeroHandle { get; private set; }

        public HeroId HeroId { get; }

        public string HeroName { get; }

        public Player Player { get; }

        public uint PlayerHandle { get; }

        public IEnumerable<Unit9> SelectedUnits
        {
            get
            {
                return this.Player.Selection.Select(x => EntityManager9.GetUnitFast(x.Handle)).Where(x => x != null);
            }
        }

        public Team Team { get; }

        public static implicit operator Hero9(Owner owner)
        {
            return owner.Hero;
        }

        public static implicit operator Unit(Owner owner)
        {
            return owner.Hero.BaseUnit;
        }

        public static implicit operator Hero(Owner owner)
        {
            return owner.Hero.BaseHero;
        }

        internal void SetHero(Unit9 myHero)
        {
            this.Hero = (Hero9)myHero;
            this.Hero.IsMyHero = true;
            this.HeroHandle = this.Hero.Handle;
        }
    }
}