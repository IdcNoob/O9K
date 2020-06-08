namespace O9K.ItemManager.Menu
{
    using System.ComponentModel.Composition;

    using Core.Managers.Context;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    [Export(typeof(IMainMenu))]
    internal class MainMenu : IMainMenu, IModule
    {
        private readonly IContext9 context;

        [ImportingConstructor]
        public MainMenu(IContext9 context)
        {
            this.context = context;
            this.RootMenu = new Menu("Item manager", "O9K.ItemManager").SetTexture(nameof(AbilityId.courier_go_to_secretshop));

            this.AutoActionsMenu = this.RootMenu.Add(new Menu("Auto actions"));
            this.AutoActionsMenu.AddTranslation(Lang.Ru, "Автоматическое использование");
            this.AutoActionsMenu.AddTranslation(Lang.Cn, "自动使用");

            this.GoldSpenderMenu = this.RootMenu.Add(new Menu("Gold spender"));
            this.GoldSpenderMenu.AddTranslation(Lang.Ru, "Трата золота");
            this.GoldSpenderMenu.AddTranslation(Lang.Cn, "金钱消费");

            this.Hotkeys = this.RootMenu.Add(new Menu("Hotkeys"));
            this.Hotkeys.AddTranslation(Lang.Ru, "Клавиши");
            this.Hotkeys.AddTranslation(Lang.Cn, "热键");

            this.AbilityLevelingMenu = this.RootMenu.Add(new Menu("Ability leveling"));
            this.AbilityLevelingMenu.AddTranslation(Lang.Ru, "Улучшение способностей");
            this.AbilityLevelingMenu.AddTranslation(Lang.Cn, "提高能力");

            this.SnatcherMenu = this.RootMenu.Add(new Menu("Snatcher"));
            this.SnatcherMenu.AddTranslation(Lang.Ru, "Снатчер");
            this.SnatcherMenu.AddTranslation(Lang.Cn, "神符抢夺");

            this.RecoveryAbuseMenu = this.RootMenu.Add(new Menu("Recovery abuse"));
            this.RecoveryAbuseMenu.AddTranslation(Lang.Ru, "Абуз восстановления");
            this.RecoveryAbuseMenu.AddTranslation(Lang.Cn, "丢物品回复");
        }

        public Menu AbilityLevelingMenu { get; }

        public Menu AutoActionsMenu { get; }

        public Menu GoldSpenderMenu { get; }

        public Menu Hotkeys { get; }

        public Menu RecoveryAbuseMenu { get; }

        public Menu RootMenu { get; }

        public Menu SnatcherMenu { get; }

        public void Activate()
        {
            this.context.MenuManager.AddRootMenu(this.RootMenu);
        }

        public void Dispose()
        {
            this.context.MenuManager.RemoveRootMenu(this.RootMenu);
        }
    }
}