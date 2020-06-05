namespace O9K.AIO.Heroes.Pudge.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class HookAllyMode : KeyPressAllyMode
    {
        private Pudge hero;

        public HookAllyMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        private Pudge Hero
        {
            get
            {
                if (this.hero == null)
                {
                    this.hero = this.UnitManager.ControllableUnits.FirstOrDefault(x => this.Owner.Hero.Handle == x.Handle) as Pudge;
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
                this.hero.HookAlly(this.TargetManager);
            }

            this.UnitManager.Orbwalk(this.hero, false, true);
        }
    }
}