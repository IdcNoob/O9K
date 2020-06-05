namespace O9K.AIO.Heroes.SpiritBreaker.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class ChargeAwayMode : KeyPressMode
    {
        private SpiritBreaker hero;

        public ChargeAwayMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
            this.LockTarget = false;
        }

        private SpiritBreaker Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as SpiritBreaker;
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

            this.hero.ChargeAway(this.TargetManager);
            this.hero.Orbwalk(this.TargetManager.Target, true, true);
        }
    }
}