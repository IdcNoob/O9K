namespace O9K.AIO.Heroes.StormSpirit.Modes
{
    using AIO.Modes.Permanent;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class ManaCalculatorModeMenu : PermanentModeMenu
    {
        public ManaCalculatorModeMenu(Menu rootMenu, string displayName, string tooltip = null)
            : base(rootMenu, displayName, tooltip)
        {
            this.ShowRemainingMp = this.Menu.Add(
                new MenuSwitcher("Remaining MP", "remainingMp" + this.SimplifiedName).SetTooltip("Show remaining or required MP"));
            this.ShowRemainingMp.AddTranslation(Lang.Ru, "Оставшаяся мана");
            this.ShowRemainingMp.AddTooltipTranslation(Lang.Ru, "Показать оставшуюся или требуемую ману");
            this.ShowRemainingMp.AddTranslation(Lang.Cn, "剩余魔法值");
            this.ShowRemainingMp.AddTooltipTranslation(Lang.Cn, "显示其余或所需的魔法值");
        }

        public MenuSwitcher ShowRemainingMp { get; }
    }
}