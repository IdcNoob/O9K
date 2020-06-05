namespace O9K.AIO.KillStealer
{
    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class KillStealMenu
    {
        private readonly MenuAbilityToggler killStealAbilityToggler;

        public KillStealMenu(Menu rootMenu)
        {
            var menu = rootMenu.Add(new Menu("Kill steal"));
            menu.AddTranslation(Lang.Ru, "Килл стил");
            menu.AddTranslation(Lang.Cn, "技能物品击杀");

            this.KillStealEnabled = menu.Add(
                new MenuSwitcher("Enabled", "killStealEnabled", true, true).SetTooltip("Enable kill steal combo"));
            this.KillStealEnabled.AddTranslation(Lang.Ru, "Включено");
            this.KillStealEnabled.AddTooltipTranslation(Lang.Ru, "Автоматически добивать героев");
            this.KillStealEnabled.AddTranslation(Lang.Cn, "启用");
            this.KillStealEnabled.AddTooltipTranslation(Lang.Cn, "启用自动斩杀连击");

            this.OverlayEnabled = menu.Add(
                new MenuSwitcher("Overlay", "killStealOverlayEnabled", true, true).SetTooltip("Show damage overlay"));
            this.OverlayEnabled.AddTranslation(Lang.Ru, "Оверлей");
            this.OverlayEnabled.AddTooltipTranslation(Lang.Ru, "Показывать полоску урона");
            this.OverlayEnabled.AddTranslation(Lang.Cn, "叠加");
            this.OverlayEnabled.AddTooltipTranslation(Lang.Cn, "显示伤害叠加");

            this.PauseOnCombo = menu.Add(
                new MenuSwitcher("Pause on combo", "pauseCombo", false, true).SetTooltip("Pause kill stealer when combo is active"));
            this.PauseOnCombo.AddTranslation(Lang.Ru, "Пауза в комбо");
            this.PauseOnCombo.AddTooltipTranslation(Lang.Ru, "Приостановить килл стил, когда используется комбо");
            this.PauseOnCombo.AddTranslation(Lang.Cn, "组合中的暂停");
            this.PauseOnCombo.AddTooltipTranslation(Lang.Cn, "连招激活时暂停斩杀");

            this.killStealAbilityToggler = menu.Add(new MenuAbilityToggler("Abilities", "killStealAbilities", null, true, true));
            this.killStealAbilityToggler.AddTranslation(Lang.Ru, "Способности");
            this.killStealAbilityToggler.AddTranslation(Lang.Cn, "技能");

            var settings = menu.Add(new Menu("Overlay settings"));
            settings.AddTranslation(Lang.Ru, "Настройки оверлея");
            settings.AddTranslation(Lang.Cn, "叠加设置");

            this.OverlayX = settings.Add(new MenuSlider("X coordinate", "xCoord", 0, -50, 50));
            this.OverlayX.AddTranslation(Lang.Ru, "X координата");
            this.OverlayX.AddTranslation(Lang.Cn, "X位置");

            this.OverlayY = settings.Add(new MenuSlider("Y coordinate", "yCoord", 0, -50, 50));
            this.OverlayY.AddTranslation(Lang.Ru, "Y координата");
            this.OverlayY.AddTranslation(Lang.Cn, "Y位置");

            this.OverlaySizeX = settings.Add(new MenuSlider("X size", "xSize", 0, -20, 20));
            this.OverlaySizeX.AddTranslation(Lang.Ru, "X размер");
            this.OverlaySizeX.AddTranslation(Lang.Cn, "X大小");

            this.OverlaySizeY = settings.Add(new MenuSlider("Y size", "ySize", 0, -20, 20));
            this.OverlaySizeY.AddTranslation(Lang.Ru, "Y размер");
            this.OverlaySizeY.AddTranslation(Lang.Cn, "Y大小");
        }

        public MenuSwitcher KillStealEnabled { get; }

        public MenuSwitcher OverlayEnabled { get; }

        public MenuSlider OverlaySizeX { get; }

        public MenuSlider OverlaySizeY { get; }

        public MenuSlider OverlayX { get; }

        public MenuSlider OverlayY { get; }

        public MenuSwitcher PauseOnCombo { get; }

        public void AddKillStealAbility(ActiveAbility ability)
        {
            this.killStealAbilityToggler.AddAbility(ability.Id);
        }

        public bool IsAbilityEnabled(string abilityName)
        {
            return this.killStealAbilityToggler.IsEnabled(abilityName);
        }
    }
}