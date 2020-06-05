namespace O9K.Evader.Settings
{
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class DebugMenu
    {
        public DebugMenu(Menu rootMenu)
        {
            var menu = new Menu("Debug");
            menu.AddTranslation(Lang.Ru, "Отладка");
            menu.AddTranslation(Lang.Cn, "调试");

            this.DrawAbilities = menu.Add(new MenuSwitcher("Draw abilities", false));
            this.DrawAbilities.AddTranslation(Lang.Ru, "Показывать способности");
            this.DrawAbilities.AddTranslation(Lang.Cn, "技能图形");

            this.DrawEvadeResult = menu.Add(new MenuSwitcher("Draw evade result", false));
            this.DrawEvadeResult.AddTranslation(Lang.Ru, "Показывать результат");
            this.DrawEvadeResult.AddTranslation(Lang.Cn, "躲避结果");

            this.DrawObstacleMap = menu.Add(new MenuSwitcher("Draw obstacles map", false));
            this.DrawObstacleMap.AddTranslation(Lang.Ru, "Показывать карту");
            this.DrawObstacleMap.AddTranslation(Lang.Cn, "障碍物图形");

            this.DrawIntersections = menu.Add(new MenuSwitcher("Draw intersections", false));
            this.DrawIntersections.AddTranslation(Lang.Ru, "Показывать пересечения");
            this.DrawIntersections.AddTranslation(Lang.Cn, "绘制交点");

            this.DrawUsableAbilities = menu.Add(new MenuSwitcher("Draw usable abilities", false));
            this.DrawUsableAbilities.AddTranslation(Lang.Ru, "Показывать свои способности");
            this.DrawUsableAbilities.AddTranslation(Lang.Cn, "可用技能显示");

            this.DrawEvadableAbilities = menu.Add(new MenuSwitcher("Draw evadable abilities", false));
            this.DrawEvadableAbilities.AddTranslation(Lang.Ru, "Показывать вражеские способности");
            this.DrawEvadableAbilities.AddTranslation(Lang.Cn, "躲避能力显示");

            rootMenu.Add(menu);
        }

        public MenuSwitcher DrawAbilities { get; }

        public MenuSwitcher DrawEvadableAbilities { get; }

        public MenuSwitcher DrawEvadeResult { get; }

        public MenuSwitcher DrawIntersections { get; }

        public MenuSwitcher DrawObstacleMap { get; }

        public MenuSwitcher DrawUsableAbilities { get; }
    }
}