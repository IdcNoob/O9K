namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.Bottle
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class BottleSettings
    {
        private readonly MenuHeroToggler heroes;

        public BottleSettings(Menu settings, IHealthRestore ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.SelfOnly = menu.GetOrAdd(new MenuSwitcher("Self only", false));
            this.SelfOnly.SetTooltip("Use only on self");
            this.SelfOnly.AddTranslation(Lang.Ru, "Только на себя");
            this.SelfOnly.AddTooltipTranslation(Lang.Ru, "Использовать способность, только на своего героя");
            this.SelfOnly.AddTranslation(Lang.Cn, "仅自己");
            this.SelfOnly.AddTooltipTranslation(Lang.Cn, "只对他的英雄使用这种能力");

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSwitcher SelfOnly { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes?.IsEnabled(name) == true;
        }
    }
}