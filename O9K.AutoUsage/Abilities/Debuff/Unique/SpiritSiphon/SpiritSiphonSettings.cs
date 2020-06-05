namespace O9K.AutoUsage.Abilities.Debuff.Unique.SpiritSiphon
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class SpiritSiphonSettings
    {
        private readonly MenuHeroToggler heroes;

        public SpiritSiphonSettings(Menu settings, IDebuff ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.OnSight = menu.GetOrAdd(new MenuSwitcher("Use on sight"));
            this.OnSight.SetTooltip("Use ability when target is visible");
            this.OnSight.AddTranslation(Lang.Ru, "Использовать сразу");
            this.OnSight.AddTooltipTranslation(Lang.Ru, "Использовать способность сразу, когда враг появляется в зоне видимости");
            this.OnSight.AddTranslation(Lang.Cn, "立即使用");
            this.OnSight.AddTooltipTranslation(Lang.Cn, "当目标出现在视线中时，立即使用该能力");

            this.OnAttack = menu.GetOrAdd(new MenuSwitcher("Use on attack", false));
            this.OnAttack.SetTooltip("Use ability when hero is attacking");
            this.OnAttack.AddTranslation(Lang.Ru, "Использовать при атаке");
            this.OnAttack.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда герой атакует");
            this.OnAttack.AddTranslation(Lang.Cn, "在攻击时使用");
            this.OnAttack.AddTooltipTranslation(Lang.Cn, "当英雄攻击时使用能力");

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold", 40, 1, 100));
            this.HpThreshold.SetTooltip("Use when hero hp% is lower");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у героя меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当英雄健康百分比较低时使用");

            this.MaxCastRange = menu.GetOrAdd(new MenuSlider("Max cast range", 0, 0, 3000));
            this.MaxCastRange.SetTooltip("Use only when enemy is in range");
            this.MaxCastRange.AddTranslation(Lang.Ru, "Максимальная дистанция");
            this.MaxCastRange.AddTooltipTranslation(Lang.Ru, "Максимальная дистанция использования способности");
            this.MaxCastRange.AddTranslation(Lang.Cn, "最大距离");
            this.MaxCastRange.AddTooltipTranslation(Lang.Cn, "使用能力的最大距离");

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider HpThreshold { get; }

        public MenuSlider MaxCastRange { get; }

        public MenuSwitcher OnAttack { get; }

        public MenuSwitcher OnSight { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}