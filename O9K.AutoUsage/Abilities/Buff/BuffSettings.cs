namespace O9K.AutoUsage.Abilities.Buff
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class BuffSettings
    {
        private readonly MenuHeroToggler heroes;

        public BuffSettings(Menu settings, IBuff ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.OnSight = menu.GetOrAdd(new MenuSwitcher("Use on sight", false));
            this.OnSight.SetTooltip("Use ability when target is visible");
            this.OnSight.AddTranslation(Lang.Ru, "Использовать сразу");
            this.OnSight.AddTooltipTranslation(Lang.Ru, "Использовать способность сразу, когда цель появляется в зоне видимости");
            this.OnSight.AddTranslation(Lang.Cn, "立即使用");
            this.OnSight.AddTooltipTranslation(Lang.Cn, "当目标出现在视线中时，立即使用该能力");

            this.OnAttack = menu.GetOrAdd(new MenuSwitcher("Use on attack"));
            this.OnAttack.SetTooltip("Use ability when hero is attacking");
            this.OnAttack.AddTranslation(Lang.Ru, "Использовать при атаке");
            this.OnAttack.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда цель атакует");
            this.OnAttack.AddTranslation(Lang.Cn, "在攻击时使用");
            this.OnAttack.AddTooltipTranslation(Lang.Cn, "当英雄攻击时使用能力");

            if (ability.BuffsAlly)
            {
                if (ability.BuffsOwner)
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

            this.HpThreshold = menu.GetOrAdd(new MenuSlider("HP% threshold (target)", 100, 1, 100));
            this.HpThreshold.SetTooltip("Use when target hp% is lower");
            this.HpThreshold.AddTranslation(Lang.Ru, "Порог ХП% (цель)");
            this.HpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда у цели меньше здоровья%");
            this.HpThreshold.AddTranslation(Lang.Cn, "生命值％（目标）");
            this.HpThreshold.AddTooltipTranslation(Lang.Cn, "当目标健康状况较差 % 时，使用能力");

            if (ability.BaseAbility.GetManaCost(0) > 0)
            {
                this.MpThreshold = menu.GetOrAdd(new MenuSlider("MP% threshold", 30, 1, 100));
                this.MpThreshold.SetTooltip("Use when ability owner mp% is higher");
                this.MpThreshold.AddTranslation(Lang.Ru, "Порог МП%");
                this.MpThreshold.AddTooltipTranslation(Lang.Ru, "Использовать способность, только если у героя больше маны%");
                this.MpThreshold.AddTranslation(Lang.Cn, "魔法值％");
                this.MpThreshold.AddTooltipTranslation(Lang.Cn, "只有当英雄有更多的法力 % 时，才使用该能力");
            }
            else
            {
                this.MpThreshold = new MenuSlider("MP% threshold", 0, 0, 0);
            }

            if (!(ability is ToggleAbility) && !string.IsNullOrEmpty(ability.BuffModifierName))
            {
                this.RenewTime = menu.GetOrAdd(new MenuSlider("Renew time", 0, 0, 5000));
                this.RenewTime.SetTooltip("Recast buff when less time remaining (ms)");
                this.RenewTime.AddTranslation(Lang.Ru, "Продление");
                this.RenewTime.AddTooltipTranslation(Lang.Ru, "Использовать способность повторно, если оставшееся время баффа меньше (мс)");
                this.RenewTime.AddTranslation(Lang.Cn, "续订时间");
                this.RenewTime.AddTooltipTranslation(Lang.Cn, "如果剩余时间较少，请使用重用功能（毫秒）");
            }
            else
            {
                this.RenewTime = new MenuSlider("Renew time", 0, 0, 0);
            }

            this.EnemiesCount = menu.GetOrAdd(new MenuSlider("Number of enemies", 1, 0, 5));
            this.EnemiesCount.SetTooltip("Use only when there are equals/more enemies near target");
            this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
            this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если врагов больше или равно");
            this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
            this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在附近有相等/更多敌人的情况下使用");

            this.Distance = menu.GetOrAdd(new MenuSlider("Distance", 650, 0, 2000));
            this.Distance.SetTooltip("Use ability only when enemy is closer");
            this.Distance.AddTranslation(Lang.Ru, "Дистанция");
            this.Distance.AddTooltipTranslation(Lang.Ru, "Использовать способность, если враг находится ближе");
            this.Distance.AddTranslation(Lang.Cn, "距离");
            this.Distance.AddTooltipTranslation(Lang.Cn, "如果敌人更近，使用能力");

            if (ability is IHasRadius)
            {
                this.AlliesCount = menu.GetOrAdd(new MenuSlider("Number of allies", 1, 1, 5));
                this.AlliesCount.SetTooltip("Use only when you can buff equals/more allies");
                this.AlliesCount.AddTranslation(Lang.Ru, "Число союзников");
                this.AlliesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если попадет по больше или равно союзников");
                this.AlliesCount.AddTranslation(Lang.Cn, "盟友数量");
                this.AlliesCount.AddTooltipTranslation(Lang.Cn, "使用能力，如果它击中更多或相等的盟友");
            }
            else
            {
                this.AlliesCount = new MenuSlider("Number of allies", 0, 0, 0);
            }

            if (ability.BuffsAlly)
            {
                this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", true));
                this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
                this.heroes.AddTranslation(Lang.Cn, "用于：");
            }
        }

        public MenuSlider AlliesCount { get; }

        public MenuSlider Distance { get; }

        public MenuSlider EnemiesCount { get; }

        public MenuSlider HpThreshold { get; }

        public MenuSlider MpThreshold { get; }

        public MenuSwitcher OnAttack { get; }

        public MenuSwitcher OnSight { get; }

        public MenuSlider RenewTime { get; }

        public MenuSwitcher SelfOnly { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes?.IsEnabled(name) == true;
        }
    }
}