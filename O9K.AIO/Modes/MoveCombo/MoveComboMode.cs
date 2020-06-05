namespace O9K.AIO.Modes.MoveCombo
{
    using Heroes.Base;

    using KeyPress;

    internal class MoveComboMode : KeyPressMode
    {
        private readonly MoveComboModeMenu menu;

        public MoveComboMode(BaseHero baseHero, MoveComboModeMenu menu)
            : base(baseHero, menu)
        {
            this.menu = menu;
        }

        protected override void ExecuteCombo()
        {
            this.UnitManager.ExecuteMoveCombo(this.menu);
            this.UnitManager.Move();
        }
    }
}