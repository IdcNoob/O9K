namespace O9K.AIO.FailSafe
{
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class FailSafeMenu
    {
        public FailSafeMenu(Menu rootMenu)
        {
            var menu = rootMenu.Add(new Menu("Fail safe"));
            menu.AddTranslation(Lang.Ru, "Отмена способностей");
            menu.AddTranslation(Lang.Cn, "防止技能失败");

            this.FailSafeEnabled = menu.Add(
                new MenuSwitcher("Enabled", "failSafeEnable", true, true).SetTooltip("Cancel ability if it won't hit the target"));
            this.FailSafeEnabled.AddTranslation(Lang.Ru, "Включено");
            this.FailSafeEnabled.AddTooltipTranslation(Lang.Ru, "Останавливать способности, которые не попадут по цели");
            this.FailSafeEnabled.AddTranslation(Lang.Cn, "启用");
            this.FailSafeEnabled.AddTooltipTranslation(Lang.Cn, "如果无法击中目标，则取消该技能");
        }

        public MenuSwitcher FailSafeEnabled { get; }
    }
}