namespace O9K.AIO.Heroes.Earthshaker.Modes
{
    using System.Collections.Generic;

    using AIO.Modes.Combo;

    using Base;

    internal class EchoSlamMode : ComboMode
    {
        public EchoSlamMode(BaseHero baseHero, IEnumerable<ComboModeMenu> comboMenus)
            : base(baseHero, comboMenus)
        {
        }
    }
}