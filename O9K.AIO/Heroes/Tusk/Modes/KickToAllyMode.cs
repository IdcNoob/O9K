namespace O9K.AIO.Heroes.Tusk.Modes
{
    using System.Collections.Generic;

    using AIO.Modes.Combo;

    using Base;

    internal class KickToAllyMode : ComboMode
    {
        public KickToAllyMode(BaseHero baseHero, IEnumerable<ComboModeMenu> comboMenus)
            : base(baseHero, comboMenus)
        {
        }
    }
}