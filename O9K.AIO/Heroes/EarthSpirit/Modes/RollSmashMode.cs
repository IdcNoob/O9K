namespace O9K.AIO.Heroes.EarthSpirit.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class RollSmashMode : KeyPressMode
    {
        private readonly RollSmashModeMenu menu;

        private EarthSpirit hero;

        public RollSmashMode(BaseHero baseHero, RollSmashModeMenu menu)
            : base(baseHero, menu)
        {
            this.menu = menu;
        }

        private EarthSpirit Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as EarthSpirit;
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
                this.hero.RollSmashCombo(this.TargetManager, this.menu);
            }

            this.UnitManager.Orbwalk(this.hero, false, true);
        }
    }
}