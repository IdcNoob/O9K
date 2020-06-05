namespace O9K.AutoUsage.Abilities.Blink
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class BlinkSettings
    {
        private readonly MenuHeroToggler heroes;

        public BlinkSettings(Menu settings, IBlink ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.UseWhenMagicImmune =
                menu.GetOrAdd(new MenuSwitcher("Magic immune", false).SetTooltip("Use ability when hero is magic immune"));
            this.UseWhenMagicImmune.AddTranslation(Lang.Ru, "Иммунитет к заклинаниям");
            this.UseWhenMagicImmune.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у героя есть иммунитет к заклинаниям");
            this.UseWhenMagicImmune.AddTranslation(Lang.Cn, "技能免疫");
            this.UseWhenMagicImmune.AddTooltipTranslation(Lang.Cn, "当英雄对咒语免疫时，使用这种能力");

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold", 40, 1, 100).SetTooltip("Use when hp% is lower"));
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, если у героя меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "如果英雄的健康状况较差， 使用能力%");

            this.Distance = menu.GetOrAdd(new MenuSlider("Distance", 450, 0, 2000).SetTooltip("Use ability only when enemy is closer"));
            this.Distance.AddTranslation(Lang.Ru, "Дистанция");
            this.Distance.AddTooltipTranslation(Lang.Ru, "Использовать способность, если враг находится ближе");
            this.Distance.AddTranslation(Lang.Cn, "距离");
            this.Distance.AddTooltipTranslation(Lang.Cn, "如果敌人更近，使用能力");

            this.EnemiesCount = menu.GetOrAdd(
                new MenuSlider("Number of enemies", 1, 0, 5).SetTooltip("Use only when there are equals/more enemies near"));
            this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
            this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если врагов больше или равно");
            this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
            this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在附近有相等/更多敌人的情况下使用");

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use when near:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать возле:");
            this.heroes.AddTranslation(Lang.Cn, "在附近使用：");
        }

        public MenuSlider Distance { get; }

        public MenuSlider EnemiesCount { get; }

        public MenuSlider HpThreshold { get; }

        public MenuSwitcher UseWhenMagicImmune { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}