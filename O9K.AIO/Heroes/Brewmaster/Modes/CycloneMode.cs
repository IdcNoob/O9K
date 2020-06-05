namespace O9K.AIO.Heroes.Brewmaster.Modes
{
    using System.Linq;

    using AIO.Modes.KeyPress;

    using Base;

    using Units;

    internal class CycloneMode : KeyPressMode
    {
        public CycloneMode(BaseHero baseHero, KeyPressModeMenu menu)
            : base(baseHero, menu)
        {
        }

        protected override void ExecuteCombo()
        {
            if (!this.TargetManager.HasValidTarget)
            {
                return;
            }

            var panda = (Storm)this.UnitManager.ControllableUnits.FirstOrDefault(x => x is Storm);
            if (panda == null)
            {
                return;
            }

            panda.CycloneTarget(this.TargetManager);
            this.UnitManager.Orbwalk(panda, false, true);
        }
    }
}