namespace O9K.AIO.Heroes.Tiny.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class TossMenu : UsableAbilityMenu
    {
        public TossMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.TossToAlly = this.Menu.Add(
                new MenuSwitcher("Toss to ally", "tossToAlly" + simplifiedName, false).SetTooltip("Toss enemy to ally"));
            this.TossToAlly.AddTranslation(Lang.Ru, "Тосс к союзнику");
            this.TossToAlly.AddTooltipTranslation(Lang.Ru, "Бросать врага к союзнику");
            this.TossToAlly.AddTranslation(Lang.Cn, LocalizationHelper.LocalizeName(AbilityId.tiny_toss) + "给盟友");
            this.TossToAlly.AddTooltipTranslation(Lang.Cn, "把敌人扔给盟友");

            this.TossAlly = this.Menu.Add(new MenuHeroToggler("Ally", true));
            this.TossAlly.AddTranslation(Lang.Ru, "Союзник");
            this.TossAlly.AddTranslation(Lang.Cn, "盟友");
        }

        public MenuHeroToggler TossAlly { get; }

        public MenuSwitcher TossToAlly { get; }
    }
}