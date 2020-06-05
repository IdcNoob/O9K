namespace O9K.AIO.TargetManager
{
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class TargetManagerMenu
    {
        public TargetManagerMenu(Menu rootMenu)
        {
            var menu = rootMenu.Add(new Menu("Target selector"));
            menu.AddTranslation(Lang.Ru, "Выбор цели");
            menu.AddTranslation(Lang.Cn, "目标选择器");

            this.FocusTarget = menu.Add(new MenuSelector("Focus target", new[] { "Near mouse", "Near hero", "Lowest health" }, true));
            this.FocusTarget.AddTranslation(Lang.Ru, "Фокусировка на");
            this.FocusTarget.AddValuesTranslation(Lang.Ru, new[] { "Ближайший к мышке", "Ближайший к герою", "Наименьшее здоровье" });
            this.FocusTarget.AddTranslation(Lang.Cn, "重点");
            this.FocusTarget.AddValuesTranslation(Lang.Cn, new[] { "靠近鼠标", "近英雄", "健康状况最低" });

            this.AdditionalTargets = menu.Add(
                new MenuSwitcher("Additional targets").SetTooltip(
                    "Allow to target units like spirit bear, wards, courier, tombstone etc."));
            this.AdditionalTargets.AddTranslation(Lang.Ru, "Выбирать дополнительные цели");
            this.AdditionalTargets.AddTooltipTranslation(Lang.Ru, "Медведь, варды, курьер и т.д.");
            this.AdditionalTargets.AddTranslation(Lang.Cn, "其他目标");
            this.AdditionalTargets.AddTooltipTranslation(Lang.Cn, "熊，病房，信使 等。");

            this.DrawTargetParticle = menu.Add(new MenuSwitcher("Draw target particle", "drawTarget", true, true));
            this.DrawTargetParticle.AddTranslation(Lang.Ru, "Рисовать линию к цели");
            this.DrawTargetParticle.AddTranslation(Lang.Cn, "绘制目标粒子特效");

            this.LockTarget = menu.Add(
                new MenuSwitcher("Lock target", "lockTarget", true, true).SetTooltip("Lock target while combo is active"));
            this.LockTarget.AddTranslation(Lang.Ru, "Фиксация цели");
            this.LockTarget.AddTooltipTranslation(Lang.Ru, "Не изменять цель в комбо");
            this.LockTarget.AddTranslation(Lang.Cn, "锁定目标");
            this.LockTarget.AddTooltipTranslation(Lang.Cn, "组合处于活动状态时锁定目标");

            this.DeathSwitch = menu.Add(
                new MenuSwitcher("Death switch", "deathSwitch", true, true).SetTooltip("Auto switch target if previous died"));
            this.DeathSwitch.AddTranslation(Lang.Ru, "Смена цели при смерти");
            this.DeathSwitch.AddTooltipTranslation(Lang.Ru, "Автоматическая смена цели, если текущая умерла");
            this.DeathSwitch.AddTranslation(Lang.Cn, "死亡开关");
            this.DeathSwitch.AddTooltipTranslation(Lang.Cn, "如果当前目标死亡，则自动更改目标");
        }

        public MenuSwitcher AdditionalTargets { get; }

        public MenuSwitcher DeathSwitch { get; }

        public MenuSwitcher DrawTargetParticle { get; }

        public MenuSelector FocusTarget { get; }

        public MenuSwitcher LockTarget { get; }
    }
}