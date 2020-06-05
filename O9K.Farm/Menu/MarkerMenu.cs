namespace O9K.Farm.Menu
{
    using O9K.Core.Entities.Abilities.Base;
    using O9K.Core.Managers.Menu;
    using O9K.Core.Managers.Menu.Items;

    internal class MarkerMenu
    {
        public MarkerMenu(Menu rootMenu)
        {
            var menu = new Menu("Marker");
            menu.AddTranslation(Lang.Ru, "Маркер");
            menu.AddTranslation(Lang.Cn, "标记");

            var attacks = menu.Add(new Menu("Attacks"));
            attacks.AddTranslation(Lang.Ru, "Атака");
            attacks.AddTranslation(Lang.Cn, "攻击");

            var attackSettings = attacks.Add(new Menu("Settings"));
            attackSettings.AddTranslation(Lang.Ru, "Настройки");
            attackSettings.AddTranslation(Lang.Cn, "设置");

            this.AttacksX = attackSettings.Add(new MenuSlider("X position", 0, -50, 50));
            this.AttacksX.AddTranslation(Lang.Ru, "X позиция");
            this.AttacksX.AddTranslation(Lang.Cn, "X位置");

            this.AttacksY = attackSettings.Add(new MenuSlider("Y position", 1, -50, 50));
            this.AttacksY.AddTranslation(Lang.Ru, "Y позиция");
            this.AttacksY.AddTranslation(Lang.Cn, "Y位置");

            this.AttacksSizeX = attackSettings.Add(new MenuSlider("X size", 0, -50, 50));
            this.AttacksSizeX.AddTranslation(Lang.Ru, "X размер");
            this.AttacksSizeX.AddTranslation(Lang.Cn, "X大小");

            this.AttacksSizeY = attackSettings.Add(new MenuSlider("Y size", -2, -20, 20));
            this.AttacksSizeY.AddTranslation(Lang.Ru, "Y размер");
            this.AttacksSizeY.AddTranslation(Lang.Cn, "Y大小");

            this.AttacksEnabled = attacks.Add(new MenuSwitcher("Enabled", true, true)).SetTooltip("Show auto attack damage");
            this.AttacksEnabled.AddTranslation(Lang.Ru, "Включено");
            this.AttacksEnabled.AddTooltipTranslation(Lang.Ru, "Паказывать урон автоатак");
            this.AttacksEnabled.AddTranslation(Lang.Cn, "启用");
            this.AttacksEnabled.AddTooltipTranslation(Lang.Cn, "显示自动攻击伤害");

            var abilities = menu.Add(new Menu("Abilities"));
            abilities.AddTranslation(Lang.Ru, "Способности");
            abilities.AddTranslation(Lang.Cn, "技能");

            var abilitySettings = abilities.Add(new Menu("Settings"));
            abilitySettings.AddTranslation(Lang.Ru, "Настройки");
            abilitySettings.AddTranslation(Lang.Cn, "设置");

            this.AbilitiesX = abilitySettings.Add(new MenuSlider("X position", 0, -50, 50));
            this.AbilitiesX.AddTranslation(Lang.Ru, "X позиция");
            this.AbilitiesX.AddTranslation(Lang.Cn, "X位置");

            this.AbilitiesY = abilitySettings.Add(new MenuSlider("Y position", 0, -50, 50));
            this.AbilitiesY.AddTranslation(Lang.Ru, "Y позиция");
            this.AbilitiesY.AddTranslation(Lang.Cn, "Y位置");

            this.AbilitiesSize = abilitySettings.Add(new MenuSlider("Size", 25, 20, 50));
            this.AbilitiesSize.AddTranslation(Lang.Ru, "Размер");
            this.AbilitiesSize.AddTranslation(Lang.Cn, "大小");

            this.Abilities = abilities.Add(new MenuAbilityPriorityChanger("Abilities"));
            this.Abilities.AddTranslation(Lang.Ru, "Способности");
            this.Abilities.AddTranslation(Lang.Cn, "技能");

            this.AbilitiesEnabled = abilities.Add(new MenuSwitcher("Enabled", true, true)).SetTooltip("Show ability damage");
            this.AbilitiesEnabled.AddTranslation(Lang.Ru, "Включено");
            this.AbilitiesEnabled.AddTooltipTranslation(Lang.Ru, "Паказывать урон способностей");
            this.AbilitiesEnabled.AddTranslation(Lang.Cn, "启用");
            this.AbilitiesEnabled.AddTooltipTranslation(Lang.Cn, "显示技能伤害");

            this.Enabled = menu.Add(new MenuSwitcher("Enabled", false, true)).SetTooltip("Enable damage markers");
            this.Enabled.AddTranslation(Lang.Ru, "Включено");
            this.Enabled.AddTooltipTranslation(Lang.Ru, "Паказывать урон");
            this.Enabled.AddTranslation(Lang.Cn, "启用");
            this.Enabled.AddTooltipTranslation(Lang.Cn, "启用伤害标记");

            rootMenu.Add(menu);
        }

        public MenuAbilityPriorityChanger Abilities { get; }

        public MenuSwitcher AbilitiesEnabled { get; }

        public MenuSlider AbilitiesSize { get; }

        public MenuSlider AbilitiesX { get; }

        public MenuSlider AbilitiesY { get; }

        public MenuSwitcher AttacksEnabled { get; }

        public MenuSlider AttacksSizeX { get; }

        public MenuSlider AttacksSizeY { get; }

        public MenuSlider AttacksX { get; }

        public MenuSlider AttacksY { get; }

        public MenuSwitcher Enabled { get; }

        public void AddAbility(Ability9 ability)
        {
            this.Abilities.AddAbility(ability.Id, !ability.IsUltimate);
        }
    }
}