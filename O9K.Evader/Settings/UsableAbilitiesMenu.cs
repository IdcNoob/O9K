namespace O9K.Evader.Settings
{
    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;

    internal class UsableAbilitiesMenu
    {
        private readonly MenuAbilityToggler blinkAbilityToggler;

        private readonly MenuAbilityToggler counterAbilityToggler;

        private readonly MenuAbilityToggler disableAbilityToggler;

        private readonly MenuAbilityToggler dodgeAbilityToggler;

        private readonly Menu menu;

        private Menu settingsMenu;

        public UsableAbilitiesMenu(Menu rootMenu)
        {
            this.menu = new Menu("Abilities");
            this.menu.AddTranslation(Lang.Ru, "Способности");
            this.menu.AddTranslation(Lang.Cn, "技能");

            this.dodgeAbilityToggler = this.menu.Add(new MenuAbilityToggler("Dodge:"));
            this.dodgeAbilityToggler.AddTranslation(Lang.Ru, "Додж:");
            this.dodgeAbilityToggler.AddTranslation(Lang.Cn, "运行:");

            this.blinkAbilityToggler = this.menu.Add(new MenuAbilityToggler("Blink:"));
            this.blinkAbilityToggler.AddTranslation(Lang.Ru, "Блинк:");
            this.blinkAbilityToggler.AddTranslation(Lang.Cn, "闪烁:");

            this.counterAbilityToggler = this.menu.Add(new MenuAbilityToggler("Counter:"));
            this.counterAbilityToggler.AddTranslation(Lang.Ru, "Контр:");
            this.counterAbilityToggler.AddTranslation(Lang.Cn, "应对:");

            this.disableAbilityToggler = this.menu.Add(new MenuAbilityToggler("Disable:"));
            this.disableAbilityToggler.AddTranslation(Lang.Ru, "Дизейбл:");
            this.disableAbilityToggler.AddTranslation(Lang.Cn, "控制:");

            rootMenu.Add(this.menu);
        }

        public Menu SettingsMenu
        {
            get
            {
                if (this.settingsMenu == null)
                {
                    this.settingsMenu = this.menu.Add(new Menu("Settings", "uniqueAbilitySettings"));
                    this.settingsMenu.AddTranslation(Lang.Ru, "Настройки");
                    this.settingsMenu.AddTranslation(Lang.Cn, "设置");
                }

                return this.settingsMenu;
            }
        }

        public Menu AddAbilitySettingsMenu(Ability9 ability)
        {
            return this.SettingsMenu.GetOrAdd(new Menu(ability.DisplayName, "uniqueSettings" + ability.Name).SetTexture(ability.Name));
        }

        public void AddBlinkAbility(Ability9 ability)
        {
            this.blinkAbilityToggler.AddAbility(ability.Name);
        }

        public void AddCounterAbility(Ability9 ability)
        {
            this.counterAbilityToggler.AddAbility(ability.Name);
        }

        public void AddDisableAbility(Ability9 ability)
        {
            this.disableAbilityToggler.AddAbility(ability.Name);
        }

        public void AddDodgeAbility(Ability9 ability)
        {
            this.dodgeAbilityToggler.AddAbility(ability.Name);
        }

        public void AddGoldSpenderAbility()
        {
            this.counterAbilityToggler.AddAbility(AbilityId.alchemist_goblins_greed);
            this.counterAbilityToggler.SetTooltip("Gold icon is for gold spender (O9K.ItemManger)");
            this.counterAbilityToggler.AddTooltipTranslation(Lang.Ru, "Иконка с золотом для траты золота (O9K.ItemManger)");
            this.counterAbilityToggler.AddTooltipTranslation(Lang.Cn, "黄金图标适用于黄金消费者 (O9K.ItemManger)");
        }

        public bool IsBlinkEnabled(string abilityName)
        {
            return this.blinkAbilityToggler.IsEnabled(abilityName);
        }

        public bool IsCounterEnabled(string abilityName)
        {
            return this.counterAbilityToggler.IsEnabled(abilityName);
        }

        public bool IsDisableEnabled(string abilityName)
        {
            return this.disableAbilityToggler.IsEnabled(abilityName);
        }

        public bool IsDodgeEnabled(string abilityName)
        {
            return this.dodgeAbilityToggler.IsEnabled(abilityName);
        }

        public bool IsGoldSpenderEnabled()
        {
            return this.counterAbilityToggler.IsEnabled(nameof(AbilityId.alchemist_goblins_greed));
        }
    }
}