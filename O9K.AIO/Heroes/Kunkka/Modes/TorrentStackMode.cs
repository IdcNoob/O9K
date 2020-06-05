namespace O9K.AIO.Heroes.Kunkka.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class TorrentStackMode : KeyPressMode
    {
        private Kunkka hero;

        public TorrentStackMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private Kunkka Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Kunkka;
                }

                return this.hero;
            }
        }

        protected override void ExecuteCombo()
        {
            this.Hero?.StackAncients();
        }
    }
}