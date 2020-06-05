namespace O9K.AutoUsage.Abilities.Autocast
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class AutocastSettings
    {
        private readonly MenuHeroToggler heroes;

        public AutocastSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));
            this.MpThreshold = menu.GetOrAdd(new MenuSlider("MP% threshold", 50, 0, 100));
            this.MpThreshold.SetTooltip("Use ability when hero has more mp%");
            this.MpThreshold.AddTranslation(Lang.Ru, "Порог МП%");
            this.MpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, только если у героя больше маны%");
            this.MpThreshold.AddTranslation(Lang.Cn, "魔法值％");
            this.MpThreshold.AddTooltipTranslation(Lang.Cn, "只有当英雄有更多的法力 % 时，才使用该能力");

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider MpThreshold { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}