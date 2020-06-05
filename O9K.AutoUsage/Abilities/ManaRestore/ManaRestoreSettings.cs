namespace O9K.AutoUsage.Abilities.ManaRestore
{
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class ManaRestoreSettings
    {
        private readonly MenuHeroToggler heroes;

        public ManaRestoreSettings(Menu settings, IManaRestore ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.MpThreshold = menu.GetOrAdd(new MenuSlider("MP% threshold (target)", 50, 1, 100));
            this.MpThreshold.SetTooltip("Use when target mp% is lower");
            this.MpThreshold.AddTranslation(Lang.Ru, "Порог МП% (цель)");
            this.MpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, только если у цели меньше маны%");
            this.MpThreshold.AddTranslation(Lang.Cn, "魔法值％（目标）)");
            this.MpThreshold.AddTooltipTranslation(Lang.Cn, "仅在目标法术力降低时使用技能");

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold", 10, 1, 100));
            this.HpThreshold.SetTooltip("Use only when ability owner hp% is higher");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП%");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у героя больше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当英雄更健康时，使用能力");

            if (ability.RestoresAlly)
            {
                if (ability.RestoresOwner)
                {
                    this.SelfOnly = menu.GetOrAdd(new MenuSwitcher("Self only", false));
                    this.SelfOnly.SetTooltip("Use only on self");
                    this.SelfOnly.AddTranslation(Lang.Ru, "Только на себя");
                    this.SelfOnly.AddTooltipTranslation(Lang.Ru, "Использовать способность, только на своего героя");
                    this.SelfOnly.AddTranslation(Lang.Cn, "仅自己");
                    this.SelfOnly.AddTooltipTranslation(Lang.Cn, "只对他的英雄使用这种能力");
                }
                else
                {
                    this.SelfOnly = new MenuSwitcher("Self only", false);
                }
            }
            else
            {
                this.SelfOnly = new MenuSwitcher("Self only");
            }

            if (ability is IHasRadius)
            {
                this.AlliesCount = menu.GetOrAdd(new MenuSlider("Number of allies", 1, 1, 5));
                this.AlliesCount.SetTooltip("Use only when you can restore equals/more allies");
                this.AlliesCount.AddTranslation(Lang.Ru, "Число союзников");
                this.AlliesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если попадет по больше или равно союзников");
                this.AlliesCount.AddTranslation(Lang.Cn, "盟友数量");
                this.AlliesCount.AddTooltipTranslation(Lang.Cn, "使用能力，如果它击中更多或相等的盟友");
            }
            else
            {
                this.AlliesCount = new MenuSlider("Number of allies", 0, 0, 0);
            }

            if (ability.RestoresAlly)
            {
                this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", true));
                this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
                this.heroes.AddTranslation(Lang.Cn, "用于：");
            }
        }

        public MenuSlider AlliesCount { get; }

        public MenuSlider HpThreshold { get; }

        public MenuSlider MpThreshold { get; }

        public MenuSwitcher SelfOnly { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes?.IsEnabled(name) == true;
        }
    }
}