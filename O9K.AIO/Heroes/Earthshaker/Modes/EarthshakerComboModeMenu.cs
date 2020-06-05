namespace O9K.AIO.Heroes.Earthshaker.Modes
{
    using AIO.Modes.Combo;

    using Core.Managers.Menu.Items;

    internal class EarthshakerComboModeMenu : ComboModeMenu
    {
        public EarthshakerComboModeMenu(Menu rootMenu, string displayName)
            : base(rootMenu, displayName)
        {
            this.PreferEnchantTotem = new MenuSwitcher("Prioritize enchant totem", "comboShakerTotem" + this.SimplifiedName, false);
            this.PreferEnchantTotem.SetTooltip("Prioritize enchant totem in combo");
            this.Menu.Add(this.PreferEnchantTotem);
        }

        public MenuSwitcher PreferEnchantTotem { get; }
    }
}