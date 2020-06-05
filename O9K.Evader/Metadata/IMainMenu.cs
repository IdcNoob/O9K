namespace O9K.Evader.Metadata
{
    using Settings;

    internal interface IMainMenu
    {
        UsableAbilitiesMenu AbilitySettings { get; }

        AlliesSettingsMenu AllySettings { get; }

        DebugMenu Debug { get; }

        EnemiesSettingsMenu EnemySettings { get; }

        HotkeysMenu Hotkeys { get; }

        SettingsMenu Settings { get; }
    }
}