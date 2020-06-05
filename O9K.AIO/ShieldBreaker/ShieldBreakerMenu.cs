namespace O9K.AIO.ShieldBreaker
{
    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class ShieldBreakerMenu
    {
        private readonly MenuAbilityToggler linkensAbilityToggler;

        private readonly MenuAbilityToggler spellShieldAbilityToggler;

        public ShieldBreakerMenu(Menu rootMenu)
        {
            var menu = new Menu("Linken's breaker", "Shield breaker");
            menu.AddTranslation(Lang.Ru, "Сбивание линки");
            menu.AddTranslation(Lang.Cn, "破坏" + LocalizationHelper.LocalizeName(AbilityId.item_sphere));

            this.linkensAbilityToggler = menu.Add(new MenuAbilityToggler("Abilities", "linkensAbilities", null, false, true));
            this.linkensAbilityToggler.AddTranslation(Lang.Ru, "Способности");
            this.linkensAbilityToggler.AddTranslation(Lang.Cn, "技能");

            this.spellShieldAbilityToggler = new MenuAbilityToggler("dummyToggler"); //ignore spell shield
            //this.spellShieldAbilityToggler =  menu.Add(new MenuAbilityToggler("Spell shield", "spellShieldAbilities", null, false, true));

            rootMenu.Add(menu);
        }

        public void AddBreakerAbility(Ability9 ability)
        {
            this.linkensAbilityToggler.AddAbility(ability.Name);
            this.spellShieldAbilityToggler.AddAbility(ability.Name);
        }

        public bool IsLinkensBreakerEnabled(string abilityName)
        {
            return this.linkensAbilityToggler.IsEnabled(abilityName);
        }

        public bool IsSpellShieldBreakerEnabled(string abilityName)
        {
            return this.spellShieldAbilityToggler.IsEnabled(abilityName);
        }
    }
}