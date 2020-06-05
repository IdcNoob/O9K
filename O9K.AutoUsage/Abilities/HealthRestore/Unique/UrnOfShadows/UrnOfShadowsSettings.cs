namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.UrnOfShadows
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class UrnOfShadowsSettings
    {
        private readonly MenuHeroToggler heroes;

        public UrnOfShadowsSettings(Menu settings, IHealthRestore ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold (target)", 50, 1, 100));
            this.HpThreshold.SetTooltip("Use when target hp% is lower");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП% (цель)");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у цели меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％（目标）");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当目标健康状况较差 % 时，使用能力");

            this.SelfOnly = menu.GetOrAdd(new MenuSwitcher("Self only", false));
            this.SelfOnly.SetTooltip("Use only on self");
            this.SelfOnly.AddTranslation(Lang.Ru, "Только на себя");
            this.SelfOnly.AddTooltipTranslation(Lang.Ru, "Использовать способность, только на своего героя");
            this.SelfOnly.AddTranslation(Lang.Cn, "仅自己");
            this.SelfOnly.AddTooltipTranslation(Lang.Cn, "只对他的英雄使用这种能力");

            this.Distance = menu.GetOrAdd(new MenuSlider("Distance", 700, 0, 2000));
            this.Distance.SetTooltip("Use when there are no enemies in range");
            this.Distance.AddTranslation(Lang.Ru, "Дистанция");
            this.Distance.AddTooltipTranslation(Lang.Ru, "Использовать способность, если врагов нету");
            this.Distance.AddTranslation(Lang.Cn, "距离");
            this.Distance.AddTooltipTranslation(Lang.Cn, "当射程内没有敌人时使用能力");

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider Distance { get; }

        public MenuSlider HpThreshold { get; }

        public MenuSwitcher SelfOnly { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}