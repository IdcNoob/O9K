namespace O9K.Evader.Settings
{
    using System;
    using System.ComponentModel.Composition;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Metadata;

    [Export(typeof(IMainMenu))]
    internal class MenuManager : IEvaderService, IMainMenu
    {
        private readonly Menu menu;

        private readonly Lazy<IMenuManager9> menuManager;

        [ImportingConstructor]
        public MenuManager(Lazy<IMenuManager9> menuManager)
        {
            this.menuManager = menuManager;

            this.menu = new Menu("Evader", "O9K.Evader").SetTexture(AbilityId.techies_minefield_sign);

            this.EnemySettings = new EnemiesSettingsMenu(this.menu);
            this.AllySettings = new AlliesSettingsMenu(this.menu);
            this.AbilitySettings = new UsableAbilitiesMenu(this.menu);
            this.Settings = new SettingsMenu(this.menu);
            this.Hotkeys = new HotkeysMenu(this.menu);
            this.Debug = new DebugMenu(this.menu);
        }

        public UsableAbilitiesMenu AbilitySettings { get; }

        public AlliesSettingsMenu AllySettings { get; }

        public DebugMenu Debug { get; }

        public EnemiesSettingsMenu EnemySettings { get; }

        public HotkeysMenu Hotkeys { get; }

        public LoadOrder LoadOrder { get; } = LoadOrder.Settings;

        public SettingsMenu Settings { get; }

        public void Activate()
        {
            this.menuManager.Value.AddRootMenu(this.menu);
        }

        public void Dispose()
        {
            this.menuManager.Value.RemoveRootMenu(this.menu);
        }
    }
}