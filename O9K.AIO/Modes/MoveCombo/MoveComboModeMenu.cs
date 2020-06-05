namespace O9K.AIO.Modes.MoveCombo
{
    using Abilities;
    using Abilities.Menus;

    using Combo;

    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using KeyPress;

    internal class MoveComboModeMenu : KeyPressModeMenu, IComboModeMenu
    {
        private readonly MenuAbilityToggler comboAbilityToggler;

        private readonly MenuAbilityToggler comboItemsToggler;

        public MoveComboModeMenu(Menu rootMenu, string displayName)
            : base(rootMenu, displayName)
        {
            this.comboAbilityToggler =
                this.Menu.Add(new MenuAbilityToggler("Abilities", "abilities" + this.SimplifiedName, null, true, true));
            this.comboAbilityToggler.AddTranslation(Lang.Ru, "Способности");
            this.comboAbilityToggler.AddTranslation(Lang.Cn, "技能");

            this.comboItemsToggler = this.Menu.Add(new MenuAbilityToggler("Items", "items" + this.SimplifiedName, null, true, true));
            this.comboItemsToggler.AddTranslation(Lang.Ru, "Предметы");
            this.comboItemsToggler.AddTranslation(Lang.Cn, "物品");
        }

        public void AddComboAbility(UsableAbility ability)
        {
            if (ability.Ability.IsItem)
            {
                this.AddComboItem(ability);
                return;
            }

            this.comboAbilityToggler.AddAbility(ability.Ability.Id);
        }

        public T GetAbilitySettingsMenu<T>(UsableAbility ability)
            where T : UsableAbilityMenu
        {
            return null;
        }

        public bool IsAbilityEnabled(IActiveAbility ability)
        {
            if (ability.IsItem)
            {
                return this.comboItemsToggler.IsEnabled(ability.Name);
            }

            return this.comboAbilityToggler.IsEnabled(ability.Name);
        }

        private void AddComboItem(UsableAbility ability)
        {
            this.comboItemsToggler.AddAbility(ability.Ability.Id);
        }
    }
}