namespace O9K.Evader.Settings
{
    using System.Collections.Generic;

    using Abilities.Base;
    using Abilities.Base.Evadable;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Evader.EvadeModes;

    internal class EnemiesSettingsMenu
    {
        private readonly Menu menu;

        public EnemiesSettingsMenu(Menu rootMenu)
        {
            this.menu = new Menu("Enemy settings");
            this.menu.AddTranslation(Lang.Ru, "Настройки врагов");
            this.menu.AddTranslation(Lang.Cn, "敌人设定");

            var itemsMenu = this.menu.Add(new Menu("Items", "items"));
            itemsMenu.AddTranslation(Lang.Ru, "Предметы");
            itemsMenu.AddTranslation(Lang.Cn, "物品");

            rootMenu.Add(this.menu);
        }

        public void AddAbility(EvadableAbility ability)
        {
            var heroMenu = this.menu.GetOrAdd(
                ability.Ability.IsItem
                    ? new Menu("Items", "items")
                    : new Menu(ability.Owner.DisplayName, ability.Owner.DefaultName).SetTexture(ability.Owner.DefaultName));

            var abilityMenu = heroMenu.GetOrAdd(
                new Menu(ability.Ability.DisplayName, ability.Ability.Name).SetTexture(ability.Ability.Name));
            var ignoreMenu = abilityMenu.GetOrAdd(new Menu("Ignore", "ignoreMenu"));
            ignoreMenu.AddTranslation(Lang.Ru, "Игнорировать");
            ignoreMenu.AddTranslation(Lang.Cn, "忽略");

            var levelIgnore = new MenuSlider("When ability level equals or lower", "levelIgnore", 0, 0, 3);
            levelIgnore.AddTranslation(Lang.Ru, "Если уровень способности меньше или равен");
            levelIgnore.AddTranslation(Lang.Cn, "如果能力级别较低或相等");
            ability.LevelIgnore = ignoreMenu.GetOrAdd(levelIgnore);

            var timeIgnore = new MenuSlider("When game time passed (minutes)", "timeIgnore", 0, 0, 60);
            timeIgnore.AddTranslation(Lang.Ru, "Если прошло игрового времени (минуты)");
            timeIgnore.AddTranslation(Lang.Cn, "如果游戏时间已过（分钟）");
            ability.TimeIgnore = ignoreMenu.GetOrAdd(timeIgnore);

            var damageIgnore = new MenuSlider("When damage% is lower", "damageIgnore", 0, 0, 100);
            damageIgnore.AddTranslation(Lang.Ru, "Если урона% (относительно здоровья) меньше");
            damageIgnore.AddTranslation(Lang.Cn, "如果损害百分比（相对于健康）较小");
            ability.DamageIgnore = ignoreMenu.GetOrAdd(damageIgnore);

            var abilityEnabled = new MenuSwitcher("Evade", "enabled");
            abilityEnabled.AddTranslation(Lang.Ru, "Уворачиваться");
            abilityEnabled.AddTranslation(Lang.Cn, "启用");
            ability.Enabled = abilityMenu.GetOrAdd(abilityEnabled);

            if (ability is IModifierCounter)
            {
                var modifierCounter = new MenuSwitcher("Modifier counter", "modifier");
                modifierCounter.AddTranslation(Lang.Ru, "Уворачиваться от модификатора");
                modifierCounter.AddTranslation(Lang.Cn, "躲避特效");
                ability.ModifierCounterEnabled = abilityMenu.GetOrAdd(modifierCounter);
            }

            var customPriority = new MenuSwitcher("Use custom priority", "customPriority", false);
            customPriority.AddTranslation(Lang.Ru, "Использовать измененный приоритет");
            customPriority.AddTranslation(Lang.Cn, "使用修改后的优先级");
            ability.UseCustomPriority = abilityMenu.GetOrAdd(customPriority);

            customPriority.ValueChange += (_, args) =>
                {
                    var abilityToggler = new MenuAbilityPriorityChanger(
                        "Custom priority",
                        "priority",
                        new Dictionary<AbilityId, bool>
                        {
                            { AbilityId.item_phase_boots, true },
                            { AbilityId.item_blink, true },
                            { AbilityId.item_cyclone, true },
                            { AbilityId.item_sheepstick, true },
                        });
                    abilityToggler.AddTranslation(Lang.Ru, "Измененный приоритет");
                    abilityToggler.AddTranslation(Lang.Cn, "优先");

                    if (args.NewValue)
                    {
                        abilityMenu.GetOrAdd(abilityToggler);
                        abilityToggler.Change += (sender, eventArgs) =>
                            {
                                ability.Priority.Clear();

                                foreach (var item in abilityToggler.Abilities)
                                {
                                    switch (item)
                                    {
                                        case nameof(AbilityId.item_sheepstick):
                                            ability.Priority.Add(EvadeMode.Disable);
                                            break;
                                        case nameof(AbilityId.item_cyclone):
                                            ability.Priority.Add(EvadeMode.Counter);
                                            break;
                                        case nameof(AbilityId.item_blink):
                                            ability.Priority.Add(EvadeMode.Blink);
                                            break;
                                        case nameof(AbilityId.item_phase_boots):
                                            ability.Priority.Add(EvadeMode.Dodge);
                                            break;
                                    }
                                }
                            };
                    }
                    else
                    {
                        abilityMenu.Remove(abilityToggler.Name);
                    }
                };
        }
    }
}