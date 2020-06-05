namespace O9K.AIO.Heroes.Magnus.Modes
{
    using AIO.Modes.KeyPress;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class BlinkSkewerModeMenu : KeyPressModeMenu
    {
        private readonly MenuHeroToggler allyToggler;

        public BlinkSkewerModeMenu(Menu rootMenu, string displayName)
            : base(rootMenu, displayName)
        {
            this.allyToggler = this.Menu.Add(new MenuHeroToggler("Skewer to ally", "skewerAllies" + this.SimplifiedName, true));
            this.allyToggler.AddTranslation(Lang.Ru, "К союзнику");
            this.allyToggler.AddTranslation(Lang.Cn, "给盟军");
        }

        public bool IsAllyEnabled(string heroName)
        {
            return this.allyToggler.IsEnabled(heroName);
        }
    }
}