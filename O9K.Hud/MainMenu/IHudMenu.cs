namespace O9K.Hud.MainMenu
{
    using Core.Managers.Menu.Items;

    internal interface IHudMenu
    {
        Menu MapMenu { get; }

        Menu MinimapSettingsMenu { get; }

        Menu NotificationsMenu { get; }

        Menu NotificationsSettingsMenu { get; }

        Menu ParticlesMenu { get; }

        Menu RootMenu { get; }

        Menu ScreenMenu { get; }

        Menu TopPanelMenu { get; }

        Menu TopPanelSettingsMenu { get; }

        Menu UniqueMenu { get; }

        Menu UnitsMenu { get; }
    }
}