namespace O9K.AutoUsage.Abilities.Nuke.Unique.Assassinate
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class AssassinateSettings
    {
        private readonly MenuHeroToggler heroes;

        public AssassinateSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.MinCastRange = menu.GetOrAdd(new MenuSlider("Min cast range", 1000, 0, 3000));
            this.MinCastRange.SetTooltip("Use ability only when enemy distance is higher");
            this.MinCastRange.AddTranslation(Lang.Ru, "Минимальная дистанция");
            this.MinCastRange.AddTooltipTranslation(Lang.Ru, "Использовать способность только если враг находится дальше");
            this.MinCastRange.AddTranslation(Lang.Cn, "最小距离");
            this.MinCastRange.AddTooltipTranslation(Lang.Cn, "仅在敌人更远时使用技能");

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider MinCastRange { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}