namespace O9K.AIO.Heroes.StormSpirit.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class OverloadChargeMode : KeyPressMode
    {
        private StormSpirit hero;

        public OverloadChargeMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private StormSpirit Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as StormSpirit;
                }

                return this.hero;
            }
        }

        protected override void ExecuteCombo()
        {
            this.Hero?.ChargeOverload();
        }
    }
}