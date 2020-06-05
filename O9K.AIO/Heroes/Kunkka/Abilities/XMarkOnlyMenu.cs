namespace O9K.AIO.Heroes.Kunkka.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class XMarkOnlyMenu : UsableAbilityMenu
    {
        public XMarkOnlyMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.XMarkOnly = this.Menu.Add(new MenuSwitcher("Use only with X mark", ability.DefaultName + "xMark" + simplifiedName));
            this.XMarkOnly.AddTranslation(Lang.Ru, "Использовать только с Х марк");
            this.XMarkOnly.AddTranslation(Lang.Cn, "仅与" + LocalizationHelper.LocalizeName(AbilityId.kunkka_x_marks_the_spot));
        }

        public MenuSwitcher XMarkOnly { get; private set; }
    }
}