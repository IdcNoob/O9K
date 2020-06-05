namespace O9K.AIO.Heroes.Riki.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class TricksOfTheTradeMenu : UsableAbilityMenu
    {
        public TricksOfTheTradeMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.SmartUsage = this.Menu.Add(
                new MenuSwitcher("Smart usage", "smart" + simplifiedName).SetTooltip(
                    "Use ability only when target is immobile or has low health"));
            this.SmartUsage.AddTranslation(Lang.Ru, "Умное использование");
            this.SmartUsage.AddTooltipTranslation(
                Lang.Ru,
                "Использовать способность только тогда, когда цель неподвижна или имеет низкое здоровье");
            this.SmartUsage.AddTranslation(Lang.Cn, "智能使用");
            this.SmartUsage.AddTooltipTranslation(Lang.Cn, "仅当目标静止不动或健康状况不佳时才使用该能力");
        }

        public MenuSwitcher SmartUsage { get; }
    }
}