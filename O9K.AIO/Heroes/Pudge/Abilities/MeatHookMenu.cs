namespace O9K.AIO.Heroes.Pudge.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class MeatHookMenu : UsableAbilityMenu
    {
        public MeatHookMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.Delay = this.Menu.Add(
                new MenuSlider("Delay (ms)", ability.DefaultName + simplifiedName, 100, 0, 500).SetTooltip(
                    "Use ability only when enemy is moving in the same direction"));
            this.Delay.AddTranslation(Lang.Ru, "Задержка (мс)");
            this.Delay.AddTooltipTranslation(Lang.Ru, "Использовать только тогда, когда враг движется в том же направлении");
            this.Delay.AddTranslation(Lang.Cn, "延迟（毫秒）");
            this.Delay.AddTooltipTranslation(Lang.Cn, "仅当敌人向同一方向移动给定时间时才使用");
        }

        public MenuSlider Delay { get; }
    }
}