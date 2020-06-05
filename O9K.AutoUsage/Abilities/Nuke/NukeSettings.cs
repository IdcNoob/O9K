namespace O9K.AutoUsage.Abilities.Nuke
{
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class NukeSettings
    {
        private readonly MenuHeroToggler heroes;

        public NukeSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            if (!ability.UnitTargetCast)
            {
                this.OnImmobileOnly = menu.GetOrAdd(new MenuSwitcher("Use on immobile only", false));
                this.OnImmobileOnly.SetTooltip("Use ability only when target is stunned/rooted");
                this.OnImmobileOnly.AddTranslation(Lang.Ru, "Только на обездвиженных");
                this.OnImmobileOnly.AddTooltipTranslation(Lang.Ru, "Использовать способность только, если враг обездвижен");
                this.OnImmobileOnly.AddTranslation(Lang.Cn, "只有在固定");
                this.OnImmobileOnly.AddTooltipTranslation(Lang.Cn, "仅在敌人无法移动时使用技能");
            }
            else
            {
                this.OnImmobileOnly = new MenuSwitcher("Use on immobile only", false);
            }

            if (ability is IHasRadius)
            {
                this.EnemiesCount = menu.GetOrAdd(new MenuSlider("Enemy count", 1, 1, 5));
                this.EnemiesCount.SetTooltip("Use ability only when you will kill equals/more enemies");
                this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
                this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если это убьет больше или равно врагов");
                this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
                this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在杀死等同/更多敌人时使用技能");
            }
            else
            {
                this.EnemiesCount = new MenuSlider("Enemy count", 0, 0, 0);
            }

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider EnemiesCount { get; }

        public MenuSwitcher OnImmobileOnly { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}