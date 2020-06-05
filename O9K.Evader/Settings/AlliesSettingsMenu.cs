namespace O9K.Evader.Settings
{
    using Core.Entities.Units;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class AlliesSettingsMenu
    {
        private readonly MenuHeroPriorityChanger allyToggler;

        private readonly MenuSwitcher prioritizeSelf;

        public AlliesSettingsMenu(Menu rootMenu)
        {
            var menu = new Menu("Ally settings");
            menu.AddTranslation(Lang.Ru, "Настройки союзников");
            menu.AddTranslation(Lang.Cn, "盟友设置");

            var helpAllies = menu.Add(new MenuSwitcher("Help allies").SetTooltip("Use abilities on allies"));
            helpAllies.AddTranslation(Lang.Ru, "Помощь союзникам");
            helpAllies.AddTooltipTranslation(Lang.Ru, "Использовать способности на союзников");
            helpAllies.AddTranslation(Lang.Cn, "帮助盟友");
            helpAllies.AddTooltipTranslation(Lang.Cn, "对盟友使用技能");
            helpAllies.ValueChange += (_, args) => this.HelpAllies = args.NewValue;

            this.prioritizeSelf = menu.Add(
                new MenuSwitcher("Prioritize self", true, true).SetTooltip("Always prioritize self when helping allies"));
            this.prioritizeSelf.AddTranslation(Lang.Ru, "Приоритет на своего героя");
            this.prioritizeSelf.AddTooltipTranslation(Lang.Ru, "Всегда спасать вначале своего героя");
            this.prioritizeSelf.AddTranslation(Lang.Cn, "优先关注你的英雄");
            this.prioritizeSelf.AddTooltipTranslation(Lang.Cn, "帮助盟友时始终优先考虑自己");

            this.allyToggler = menu.Add(new MenuHeroPriorityChanger("Allies", true));
            this.allyToggler.AddTranslation(Lang.Ru, "Союзники");
            this.allyToggler.AddTranslation(Lang.Cn, "盟友");

            rootMenu.Add(menu);
        }

        public bool HelpAllies { get; private set; }

        public int GetOrder(Unit9 hero)
        {
            if (this.prioritizeSelf && hero.IsMyHero)
            {
                return int.MinValue;
            }

            return this.allyToggler.GetPriority(hero.Name);
        }

        public bool IsEnabled(string heroName)
        {
            return this.allyToggler.IsEnabled(heroName);
        }
    }
}