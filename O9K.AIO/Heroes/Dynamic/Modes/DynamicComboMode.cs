namespace O9K.AIO.Heroes.Dynamic.Modes
{
    using AIO.Modes.Combo;

    using Base;

    internal class DynamicComboMode : ComboMode
    {
        public DynamicComboMode(BaseHero baseHero, params ComboModeMenu[] comboMenus)
            : base(baseHero, comboMenus)
        {
        }
    }
}