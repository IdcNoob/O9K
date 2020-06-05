namespace O9K.AIO.Heroes.Magnus.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class BlinkSkewerMode : KeyPressMode
    {
        private readonly BlinkSkewerModeMenu menu;

        private Magnus hero;

        public BlinkSkewerMode(BaseHero baseHero, BlinkSkewerModeMenu menu)
            : base(baseHero, menu)
        {
            this.menu = menu;
        }

        private Magnus Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Magnus;
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
                this.hero.BlinkSkewerCombo(this.TargetManager, this.menu);
            }

            this.UnitManager.Orbwalk(this.hero, false, true);
        }
    }
}