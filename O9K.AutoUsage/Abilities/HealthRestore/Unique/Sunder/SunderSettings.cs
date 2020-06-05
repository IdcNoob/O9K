namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.Sunder
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class SunderSettings
    {
        public SunderSettings(Menu settings, IHealthRestore ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold", 30, 1, 100));
            this.HpThreshold.SetTooltip("Use when hero hp% is lower");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у героя меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当英雄健康百分比较低时使用");

            this.TargetHpThreshold = menu.GetOrAdd(new MenuSlider("Target HP% threshold", 50, 1, 100));
            this.TargetHpThreshold.SetTooltip("Use when target hp% is higher");
            this.TargetHpThreshold.AddTranslation(Lang.Ru, "Порог ХП% цели");
            this.TargetHpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у цели больше здоровья%");
            this.TargetHpThreshold.AddTranslation(Lang.Cn, "生命值％（目标）");
            this.TargetHpThreshold.AddTooltipTranslation(Lang.Cn, "当目标具有更健康 % 时，使用该能力");

            this.EnemiesCount = menu.GetOrAdd(new MenuSlider("Number of enemies", 1, 0, 5));
            this.EnemiesCount.SetTooltip("Use only when there are equals/more enemies near target");
            this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
            this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если врагов больше или равно");
            this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
            this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在附近有相等/更多敌人的情况下使用");

            this.Distance = menu.GetOrAdd(new MenuSlider("Distance", 900, 0, 2000));
            this.Distance.SetTooltip("Use ability only when enemy is closer");
            this.Distance.AddTranslation(Lang.Ru, "Дистанция");
            this.Distance.AddTooltipTranslation(Lang.Ru, "Использовать способность, если враг находится ближе");
            this.Distance.AddTranslation(Lang.Cn, "距离");
            this.Distance.AddTooltipTranslation(Lang.Cn, "如果敌人更近，使用能力");

            this.UseOnIllusions = menu.GetOrAdd(new MenuSwitcher("Use on illusions"));
            this.UseOnIllusions.AddTranslation(Lang.Ru, "Использовать на иллюзии");
            this.UseOnIllusions.AddTranslation(Lang.Cn, "用于错觉");

            this.UseOnAllies = menu.GetOrAdd(new MenuSwitcher("Use on allies", false));
            this.UseOnAllies.AddTranslation(Lang.Ru, "Использовать на союзников");
            this.UseOnAllies.AddTranslation(Lang.Cn, "用于盟友");
        }

        public MenuSlider Distance { get; }

        public MenuSlider EnemiesCount { get; }

        public MenuSlider HpThreshold { get; }

        public MenuSlider TargetHpThreshold { get; }

        public MenuSwitcher UseOnAllies { get; }

        public MenuSwitcher UseOnIllusions { get; }
    }
}