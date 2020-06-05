namespace O9K.AIO.Heroes.Ursa.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class EnrageMenu : UsableAbilityMenu
    {
        public EnrageMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.StacksCount = this.Menu.Add(
                new MenuSlider("Stacks count", ability.DefaultName + "stacks" + simplifiedName, 3, 0, 10).SetTooltip(
                    "Use ability only if enemy has equals/more fury swipes stacks"));
            this.StacksCount.AddTranslation(Lang.Ru, "Количество стаков");
            this.StacksCount.AddTooltipTranslation(
                Lang.Ru,
                "Использовать способность, только если у врага есть больше стаков "
                + LocalizationHelper.LocalizeName(AbilityId.ursa_fury_swipes));
            this.StacksCount.AddTranslation(Lang.Cn, "堆栈计数");
            this.StacksCount.AddTooltipTranslation(
                Lang.Cn,
                "仅当敌人有更多的堆栈时，才使用该能力" + LocalizationHelper.LocalizeName(AbilityId.ursa_fury_swipes));
        }

        public MenuSlider StacksCount { get; }
    }
}