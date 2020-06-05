namespace O9K.AutoUsage.Settings
{
    using System.Collections.Generic;

    using Abilities;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class GroupSettings
    {
        private static readonly Dictionary<string, string> cnLoc = new Dictionary<string, string>
        {
            { "Kill steal", "技能物品击杀" },
            { "Disable", "控制" },
            { "Blink", "闪烁" },
            { "Shield", "护盾" },
            { "Health restore", "治疗" },
            { "Mana restore", "补充魔法" },
            { "Debuff", "诅咒" },
            { "Buffs", "加强" },
            { "Special", "特殊" },
            { "Linken\'s break", "破坏" + LocalizationHelper.LocalizeName(AbilityId.item_sphere) },
            { "Autocast", "自动施法" },
        };

        private static readonly Dictionary<string, string> ruLoc = new Dictionary<string, string>
        {
            { "Kill steal", "Килл стил" },
            { "Disable", "Дизейбл" },
            { "Blink", "Блинк" },
            { "Shield", "Щит" },
            { "Health restore", "Восстановление ХП" },
            { "Mana restore", "Восстановление МП" },
            { "Debuff", "Дебафф" },
            { "Buffs", "Бафф" },
            { "Special", "Особый" },
            { "Linken\'s break", "Сбивание линки" },
            { "Autocast", "Автокаст" },
        };

        private readonly Menu mainMenu;

        private readonly Menu settingsMenu;

        private bool settingsMenuAdded;

        public GroupSettings(Menu mainMenu, Menu settingsMenu, string name, int defaultUpdateRate, bool defaultAbilityValue = true)
        {
            this.mainMenu = mainMenu;
            this.settingsMenu = settingsMenu;

            this.Menu = new Menu(name);
            this.AbilityToggler = new MenuAbilityPriorityChanger(name, null, defaultAbilityValue);

            if (ruLoc.TryGetValue(name, out var loc))
            {
                this.Menu.AddTranslation(Lang.Ru, loc);
                this.AbilityToggler.AddTranslation(Lang.Ru, loc);
            }

            if (cnLoc.TryGetValue(name, out loc))
            {
                this.Menu.AddTranslation(Lang.Cn, loc);
                this.AbilityToggler.AddTranslation(Lang.Cn, loc);
            }

            this.UseWhenInvisible = this.Menu.Add(new MenuSwitcher("Invisible")).SetTooltip("Use abilities when hero is invisible");
            this.UseWhenInvisible.AddTranslation(Lang.Ru, "Невидимость");
            this.UseWhenInvisible.AddTooltipTranslation(Lang.Ru, "Использовать способности, когда герой невидим");
            this.UseWhenInvisible.AddTranslation(Lang.Cn, "隐身时");
            this.UseWhenInvisible.AddTooltipTranslation(Lang.Cn, "英雄不可见时使用技能");

            this.GroupEnabled = this.Menu.Add(new MenuSwitcher("Enabled")).SetTooltip("Enable ability group");
            this.GroupEnabled.AddTranslation(Lang.Ru, "Включено");
            this.GroupEnabled.AddTooltipTranslation(Lang.Ru, "Включить способности группы");
            this.GroupEnabled.AddTranslation(Lang.Cn, "启用");
            this.GroupEnabled.AddTooltipTranslation(Lang.Cn, "启用能力组");

            this.UpdateRate = this.Menu.Add(
                new MenuSlider("Update rate", defaultUpdateRate, 0, 1000).SetTooltip(
                    "Lower value will increase reaction time, but reduce fps"));
            this.UpdateRate.AddTranslation(Lang.Ru, "Частота обновления");
            this.UpdateRate.AddTooltipTranslation(Lang.Ru, "Низкое значение ускорит срабатывание способностей, но снизит фпс");
            this.UpdateRate.AddTranslation(Lang.Cn, "更新率");
            this.UpdateRate.AddTooltipTranslation(Lang.Cn, "值将加速能力触发，但会减少 fps");
        }

        public MenuAbilityPriorityChanger AbilityToggler { get; }

        public MenuSwitcher GroupEnabled { get; }

        public Menu Menu { get; }

        public MenuSlider UpdateRate { get; }

        public MenuSwitcher UseWhenInvisible { get; }

        public void AddAbility(UsableAbility ability)
        {
            if (!this.settingsMenuAdded)
            {
                this.AddSettingsMenu();
            }

            this.AbilityToggler.AddAbility(ability.Ability.Name);

            if (this.AbilityToggler.IsEnabled(ability.Ability.Name))
            {
                ability.Enabled(true);
            }
        }

        public virtual void AddSettingsMenu()
        {
            this.settingsMenu.Add(this.Menu);
            this.mainMenu.Add(this.AbilityToggler);
            this.settingsMenuAdded = true;
        }
    }
}