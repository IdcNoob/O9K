namespace O9K.AIO.Menu
{
    using System;

    using Core.Entities.Heroes;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class MenuManager : IDisposable
    {
        private readonly IMenuManager9 menuManager;

        public MenuManager(Owner owner, IMenuManager9 menuManager)
        {
            this.menuManager = menuManager;
            this.RootMenu = new Menu("AIO", "O9K.AIO").SetTexture(owner.HeroName);

            this.GeneralSettingsMenu = this.RootMenu.Add(new Menu("General settings"));
            this.GeneralSettingsMenu.AddTranslation(Lang.Ru, "Основные настройки");
            this.GeneralSettingsMenu.AddTranslation(Lang.Cn, "常规设置");

            this.Enabled = this.GeneralSettingsMenu.Add(
                new MenuSwitcher("Enabled", true, true).SetTooltip("Enable assembly for " + owner.HeroDisplayName));
            this.Enabled.AddTranslation(Lang.Ru, "Включено");
            this.Enabled.AddTooltipTranslation(Lang.Ru, "Включить скрипт для " + owner.HeroDisplayName);
            this.Enabled.AddTranslation(Lang.Cn, "启用");
            this.Enabled.AddTooltipTranslation(Lang.Cn, "为" + owner.HeroDisplayName + "启用脚本");

            menuManager.AddRootMenu(this.RootMenu);
        }

        public MenuSwitcher Enabled { get; }

        public Menu GeneralSettingsMenu { get; }

        public Menu RootMenu { get; }

        public void Dispose()
        {
            this.menuManager.RemoveRootMenu(this.RootMenu);
        }
    }
}