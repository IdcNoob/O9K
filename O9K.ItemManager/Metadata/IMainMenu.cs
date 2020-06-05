namespace O9K.ItemManager.Metadata
{
    using Core.Managers.Menu.Items;

    internal interface IMainMenu
    {
        Menu AbilityLevelingMenu { get; }

        Menu AbyssalAbuseMenu { get; }

        Menu AutoActionsMenu { get; }

        Menu GoldSpenderMenu { get; }

        Menu Hotkeys { get; }

        Menu RecoveryAbuseMenu { get; }

        Menu RootMenu { get; }

        Menu SnatcherMenu { get; }
    }
}