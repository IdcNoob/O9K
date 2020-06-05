namespace O9K.AutoUsage.Abilities.Disable
{
    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Extensions;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;
    using Core.Prediction.Collision;

    using Ensage;

    internal class DisableSettings
    {
        private readonly MenuHeroToggler heroes;

        public DisableSettings(Menu settings, IDisable ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            var stackMenu = menu.GetOrAdd(new Menu("Stack"));
            stackMenu.AddTranslation(Lang.Ru, "Наложение");
            stackMenu.AddTranslation(Lang.Cn, "覆盖");

            this.HexStack = stackMenu.GetOrAdd(new MenuSwitcher("Stack with hex", false));
            this.HexStack.AddTranslation(Lang.Ru, "Использовать с хексом");
            this.HexStack.AddTranslation(Lang.Cn, "与妖术一起使用");

            this.SilenceStack = stackMenu.GetOrAdd(new MenuSwitcher("Stack with silence", false));
            this.SilenceStack.AddTranslation(Lang.Ru, "Использовать с сайленсом");
            this.SilenceStack.AddTranslation(Lang.Cn, "与沉默一起使用");

            this.StunStack = stackMenu.GetOrAdd(new MenuSwitcher("Stack with stun", false));
            this.StunStack.AddTranslation(Lang.Ru, "Использовать с станом");
            this.StunStack.AddTranslation(Lang.Cn, "与眩晕一起使用");

            this.DisarmStack = stackMenu.GetOrAdd(new MenuSwitcher("Stack with disarm", false));
            this.DisarmStack.AddTranslation(Lang.Ru, "Использовать с дизармом");
            this.DisarmStack.AddTranslation(Lang.Cn, "与缴械一起使用");

            this.RootStack = stackMenu.GetOrAdd(new MenuSwitcher("Stack with root", false));
            this.RootStack.AddTranslation(Lang.Ru, "Использовать с рутом");
            this.RootStack.AddTranslation(Lang.Cn, "与缠绕一起使用");

            this.OnSight = menu.GetOrAdd(new MenuSwitcher("Use on sight", false));
            this.OnSight.SetTooltip("Use ability when enemy is visible");
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

            this.OnCast = menu.GetOrAdd(new MenuSwitcher("Use on cast", false));
            this.OnCast.SetTooltip("Use ability when enemy is casting");
            this.OnCast.AddTranslation(Lang.Ru, "Использовать при касте");
            this.OnCast.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда враг начинает кастовать");
            this.OnCast.AddTranslation(Lang.Cn, "铸造时使用");
            this.OnCast.AddTooltipTranslation(Lang.Cn, "使用能力，当敌人铸造");

            if (!ability.IsDisarm(false))
            {
                this.OnChannel = menu.GetOrAdd(new MenuSwitcher("Use on channel"));
                this.OnChannel.SetTooltip("Use ability when enemy is channeling");
                this.OnChannel.AddTranslation(Lang.Ru, "Использовать при ченнелинге");
                this.OnChannel.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда враг ченнелит");
                this.OnChannel.AddTranslation(Lang.Cn, "持续施法时使用");
                this.OnChannel.AddTooltipTranslation(Lang.Cn, "当敌人是持续施法时使用能力");
            }
            else
            {
                this.OnChannel = new MenuSwitcher("Use on channel", false);
            }

            this.OnInitiation = menu.GetOrAdd(new MenuSwitcher("Use on initiation", false));
            this.OnInitiation.SetTooltip("Use ability when enemy has blinked in");
            this.OnInitiation.AddTranslation(Lang.Ru, "Использовать при инициации");
            this.OnInitiation.AddTooltipTranslation(Lang.Ru, "Использовать способность, когда враг блинканулся");
            this.OnInitiation.AddTranslation(Lang.Cn, "启动时使用");
            this.OnInitiation.AddTooltipTranslation(Lang.Cn, "当敌人眨眼时使用能力");

            if ((ability.AppliesUnitState & UnitState.Invulnerable) == 0)
            {
                this.OnChainStun = menu.GetOrAdd(new MenuSwitcher("Use to chain stun"));
                this.OnChainStun.SetTooltip("Use ability to chain stun enemy");
                this.OnChainStun.AddTranslation(Lang.Ru, "Использовать для чейн стана");
                this.OnChainStun.AddTooltipTranslation(Lang.Ru, "Использовать способность, чтобы чейн станить врага");
                this.OnChainStun.AddTranslation(Lang.Cn, "正在被控制");
                this.OnChainStun.AddTooltipTranslation(Lang.Cn, "用于控制敌人");
            }
            else
            {
                this.OnChainStun = new MenuSwitcher("Use to chain stun", false);
            }

            this.Delay = menu.GetOrAdd(new MenuSlider("Delay (ms)", ability.CastPoint <= 0 ? 150 : 0, 0, 500));
            this.Delay.SetTooltip("Delay before using ability");
            this.Delay.AddTranslation(Lang.Ru, "Задержка (мс)");
            this.Delay.AddTooltipTranslation(Lang.Ru, "Задержка перед использованием способности");
            this.Delay.AddTranslation(Lang.Cn, "延迟（毫秒）");
            this.Delay.AddTooltipTranslation(Lang.Cn, "使用能力前的延迟");

            this.MaxCastRange = menu.GetOrAdd(new MenuSlider("Max cast range", 0, 0, 3000));
            this.MaxCastRange.SetTooltip("Use ability only when enemy is in range");
            this.MaxCastRange.AddTranslation(Lang.Ru, "Максимальная дистанция");
            this.MaxCastRange.AddTooltipTranslation(Lang.Ru, "Максимальная дистанция использования способности");
            this.MaxCastRange.AddTranslation(Lang.Cn, "最大距离");
            this.MaxCastRange.AddTooltipTranslation(Lang.Cn, "使用能力的最大距离");

            if (ability is IHasRadius && ability.CollisionTypes == CollisionTypes.None)
            {
                this.EnemiesCount = menu.GetOrAdd(new MenuSlider("Number of enemies", 1, 1, 5));
                this.EnemiesCount.SetTooltip("Use only when you can disable equals/more enemies");
                this.EnemiesCount.AddTranslation(Lang.Ru, "Число врагов");
                this.EnemiesCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, если попадет по больше или равно врагов");
                this.EnemiesCount.AddTranslation(Lang.Cn, "敌人数量");
                this.EnemiesCount.AddTooltipTranslation(Lang.Cn, "仅在附近有相等/更多敌人的情况下使用");
            }
            else
            {
                this.EnemiesCount = new MenuSlider("Number of enemies", 0, 0, 0);
            }

            this.heroes = menu.GetOrAdd(new MenuHeroToggler("Use on:", false, true));
            this.heroes.AddTranslation(Lang.Ru, "Использовать на:");
            this.heroes.AddTranslation(Lang.Cn, "用于：");
        }

        public MenuSlider Delay { get; }

        public MenuSwitcher DisarmStack { get; }

        public MenuSlider EnemiesCount { get; }

        public MenuSwitcher HexStack { get; }

        public MenuSlider MaxCastRange { get; }

        public MenuSwitcher OnAttack { get; }

        public MenuSwitcher OnCast { get; }

        public MenuSwitcher OnChainStun { get; }

        public MenuSwitcher OnChannel { get; }

        public MenuSwitcher OnInitiation { get; }

        public MenuSwitcher OnSight { get; }

        public MenuSwitcher RootStack { get; }

        public MenuSwitcher SilenceStack { get; }

        public MenuSwitcher StunStack { get; }

        public bool IsHeroEnabled(string name)
        {
            return this.heroes.IsEnabled(name);
        }
    }
}