namespace O9K.AIO.Heroes.Windranger.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class ShackleshotMenu : UsableAbilityMenu
    {
        public ShackleshotMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.MoveToShackle = this.Menu.Add(
                new MenuSwitcher("Move to shackle", "shackleMove" + simplifiedName).SetTooltip(
                    "Auto position your hero to shackle the enemy"));
            this.MoveToShackle.AddTranslation(Lang.Ru, "Двигаться");
            this.MoveToShackle.AddTooltipTranslation(
                Lang.Ru,
                "Автоматически двигаться чтобы использовать " + LocalizationHelper.LocalizeName(AbilityId.windrunner_shackleshot));
            this.MoveToShackle.AddTranslation(Lang.Cn, "移动");
            this.MoveToShackle.AddTooltipTranslation(
                Lang.Cn,
                "自动移动以使用" + LocalizationHelper.LocalizeName(AbilityId.windrunner_shackleshot));
        }

        public MenuSwitcher MoveToShackle { get; }
    }
}