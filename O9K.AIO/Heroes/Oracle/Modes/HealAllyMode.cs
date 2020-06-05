namespace O9K.AIO.Heroes.Oracle.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class HealAllyMode : KeyPressAllyMode
    {
        private Oracle hero;

        public HealAllyMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private Oracle Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Oracle;
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

            if (this.TargetManager.HasValidTarget)
            {
                this.hero.HealAllyCombo(this.TargetManager);
            }

            this.UnitManager.Orbwalk(this.hero, false, true);
        }
    }
}