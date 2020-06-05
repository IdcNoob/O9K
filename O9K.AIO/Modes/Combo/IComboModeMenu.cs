namespace O9K.AIO.Modes.Combo
{
    using Abilities;
    using Abilities.Menus;

    using Core.Entities.Abilities.Base.Components.Base;

    internal interface IComboModeMenu
    {
        T GetAbilitySettingsMenu<T>(UsableAbility ability)
            where T : UsableAbilityMenu;

        bool IsAbilityEnabled(IActiveAbility ability);
    }
}