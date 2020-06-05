namespace O9K.AutoUsage.Abilities.LinkensBreak
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class LinkensBreakSettings
    {
        private readonly MenuHeroToggler heroes;

        public LinkensBreakSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.BreakSpellShield = new MenuSwitcher("Break spell shield", false);
            //this.BreakSpellShield = menu.GetOrAdd(new MenuSwitcher("Break spell shield", false));
            //this.BreakSpellShield.SetTooltip("Break anti mage\'s spell shield");

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

        public MenuSwitcher BreakSpellShield { get; }

        public MenuSlider MaxCastRange { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}