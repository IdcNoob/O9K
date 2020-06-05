namespace O9K.AIO.Heroes.Kunkka.Modes
{
    using System.Linq;

    using AIO.Modes.Permanent;

    using Base;

    using Units;

    internal class AutoReturnMode : PermanentMode
    {
        private Kunkka hero;

        public AutoReturnMode(BaseHero baseHero, PermanentModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private Kunkka Hero
        {
            get
            {
                if (this.hero?.IsValid != true)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Kunkka;
                }

                return this.hero;
            }
        }

        protected override void Execute()
        {
            if (this.Hero == null || !this.hero.Owner.IsAlive)
            {
                return;
            }

            this.hero.AutoReturn();
        }
    }
}