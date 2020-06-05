namespace O9K.AIO.Heroes.Phoenix.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class SunRayAllyMode : KeyPressAllyMode
    {
        private Phoenix hero;

        public SunRayAllyMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private Phoenix Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Phoenix;
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
                this.hero.SunRayAllyCombo(this.TargetManager);
            }

            this.UnitManager.Orbwalk(this.hero, false, true);
        }
    }
}