namespace O9K.Evader.Settings
{
    using System;
    using System.Collections.Generic;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;

    using Evader.EvadeModes;

    internal class SettingsMenu
    {
        private readonly MenuAbilityPriorityChanger defaultPriority;

        public SettingsMenu(Menu rootMenu)
        {
            var menu = new Menu("Settings");
            menu.AddTranslation(Lang.Ru, "Настройки");
            menu.AddTranslation(Lang.Cn, "设置");

            this.defaultPriority = menu.Add(
                new MenuAbilityPriorityChanger(
                    "Default priority",
                    new Dictionary<AbilityId, bool>
                    {
                        { AbilityId.item_phase_boots, true },
                        { AbilityId.item_blink, true },
                        { AbilityId.item_cyclone, true },
                        { AbilityId.item_sheepstick, true },
                    }));
            this.defaultPriority.AddTranslation(Lang.Ru, "Приоритет");
            this.defaultPriority.AddTranslation(Lang.Cn, "默认优先级");
            this.defaultPriority.Change += this.DefaultPriorityOnValueChanged;

            var modifierAllyCounter = menu.Add(
                new MenuSwitcher("Modifier counter").SetTooltip("Use abilities (shields, heals) on allies/self/enemies"));
            modifierAllyCounter.AddTranslation(Lang.Ru, "Уворачиваться от модификаторов");
            modifierAllyCounter.AddTooltipTranslation(Lang.Ru, "Использовать способности против баффов/дебаффов на союзников/врагов");
            modifierAllyCounter.AddTranslation(Lang.Cn, "躲避特效");
            modifierAllyCounter.AddTooltipTranslation(Lang.Cn, "对盟友/自身/敌人使用能力（盾牌，治疗）");
            modifierAllyCounter.ValueChange += (sender, args) => this.ModifierCounter = args.NewValue;

            var multiUnitControl = menu.Add(new MenuSwitcher("Multi unit control").SetTooltip("Use all controllable units"));
            multiUnitControl.AddTranslation(Lang.Ru, "Контроль всех юнитов");
            multiUnitControl.AddTooltipTranslation(Lang.Ru, "Использовать всех своих юнитов для уворота");
            multiUnitControl.AddTranslation(Lang.Cn, "多英雄控制");
            multiUnitControl.AddTooltipTranslation(Lang.Cn, "使用所有可控单元");
            multiUnitControl.ValueChange += (sender, args) => this.MultiUnitControl = args.NewValue;

            rootMenu.Add(menu);
        }

        public List<EvadeMode> DefaultPriority { get; } = new List<EvadeMode>();

        public bool ModifierCounter { get; private set; }

        public bool MultiUnitControl { get; private set; }

        private void DefaultPriorityOnValueChanged(object sender, EventArgs eventArgs)
        {
            this.DefaultPriority.Clear();

            foreach (var item in this.defaultPriority.Abilities)
            {
                switch (item)
                {
                    case "item_sheepstick":
                        this.DefaultPriority.Add(EvadeMode.Disable);
                        break;
                    case "item_cyclone":
                        this.DefaultPriority.Add(EvadeMode.Counter);
                        break;
                    case "item_blink":
                        this.DefaultPriority.Add(EvadeMode.Blink);
                        break;
                    case "item_phase_boots":
                        this.DefaultPriority.Add(EvadeMode.Dodge);
                        break;
                }
            }
        }
    }
}