namespace O9K.AIO.Heroes.EarthSpirit.Modes
{
    using AIO.Modes.KeyPress;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class RollSmashModeMenu : KeyPressModeMenu
    {
        private readonly MenuHeroToggler allyToggler;

        public RollSmashModeMenu(Menu rootMenu, string displayName)
            : base(rootMenu, displayName)
        {
            this.allyToggler = this.Menu.Add(new MenuHeroToggler("Smash to ally", "smashAllies" + this.SimplifiedName, true));
            this.allyToggler.AddTranslation(Lang.Ru, "Использовать пинок к союзнику");
            this.allyToggler.AddTranslation(Lang.Cn, LocalizationHelper.LocalizeName(AbilityId.earth_spirit_boulder_smash) + "给盟友");
        }

        public bool IsAllyEnabled(string heroName)
        {
            return this.allyToggler.IsEnabled(heroName);
        }
    }
}