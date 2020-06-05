namespace O9K.AutoUsage.Settings
{
    using System;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class MainSettings : IDisposable
    {
        private readonly IMenuManager9 menuManager;

        public MainSettings(IMenuManager9 menuManager)
        {
            this.menuManager = menuManager;

            this.Menu = new Menu("Auto usage", "O9K.AutoUsage").SetTexture("techies_focused_detonate");
            var settingsMenu = this.Menu.Add(new Menu("Settings"));
            settingsMenu.AddTranslation(Lang.Ru, "Настройки");
            settingsMenu.AddTranslation(Lang.Cn, "设置");

            this.NukeSettings = new GroupSettings(this.Menu, settingsMenu, "Kill steal", 200);
            this.DisableSettings = new GroupSettings(this.Menu, settingsMenu, "Disable", 75);
            this.BlinkSettings = new GroupSettings(this.Menu, settingsMenu, "Blink", 75);
            this.ShieldSettings = new GroupSettings(this.Menu, settingsMenu, "Shield", 100);
            this.HpRestoreSettings = new GroupSettings(this.Menu, settingsMenu, "Health restore", 75);
            this.MpRestoreSettings = new GroupSettings(this.Menu, settingsMenu, "Mana restore", 500);
            this.DebuffSettings = new GroupSettings(this.Menu, settingsMenu, "Debuff", 100);
            this.BuffSettings = new GroupSettings(this.Menu, settingsMenu, "Buffs", 200);
            this.SpecialSettings = new GroupSettings(this.Menu, settingsMenu, "Special", 200);
            this.LinkensBreakSettings = new GroupSettings(this.Menu, settingsMenu, "Linken\'s break", 300, false);
            this.AutocastSettings = new GroupSettings(this.Menu, settingsMenu, "Autocast", 0);

            menuManager.AddRootMenu(this.Menu);
        }

        public GroupSettings AutocastSettings { get; }

        public GroupSettings BlinkSettings { get; }

        public GroupSettings BuffSettings { get; }

        public GroupSettings DebuffSettings { get; }

        public GroupSettings DisableSettings { get; }

        public GroupSettings HpRestoreSettings { get; }

        public GroupSettings LinkensBreakSettings { get; }

        public Menu Menu { get; }

        public GroupSettings MpRestoreSettings { get; }

        public GroupSettings NukeSettings { get; }

        public GroupSettings ShieldSettings { get; }

        public GroupSettings SpecialSettings { get; }

        public void Dispose()
        {
            this.menuManager.RemoveRootMenu(this.Menu);
        }
    }
}