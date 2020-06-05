namespace O9K.AIO.Heroes.Tiny.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class TossUnderTowerMode : KeyPressMode
    {
        private Tiny hero;

        public TossUnderTowerMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private Tiny Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Tiny;
                }

                return this.hero;
            }
        }

        protected override void ExecuteCombo()
        {
            if (this.Hero == null)
            {
                return;
            }

            if (!this.hero.Owner.IsAlive)
            {
                return;
            }

            this.hero.Toss();
        }
    }
}