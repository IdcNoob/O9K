namespace O9K.Evader.Settings
{
    using System.Windows.Input;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class HotkeysMenu
    {
        public HotkeysMenu(Menu rootMenu)
        {
            var menu = new Menu("Hotkeys");
            menu.AddTranslation(Lang.Ru, "Клавиши");
            menu.AddTranslation(Lang.Cn, "热键");

            this.PathfinderMode = menu.Add(new MenuHoldKey("Change dodge mode").SetTooltip("Dodge: all; only disables; none"));
            this.PathfinderMode.AddTranslation(Lang.Ru, "Изменить режим доджа");
            this.PathfinderMode.AddTooltipTranslation(Lang.Ru, "Доджить все, только станы или ничего");
            this.PathfinderMode.AddTranslation(Lang.Cn, "更改运行模式");
            this.PathfinderMode.AddTooltipTranslation(Lang.Cn, "运行：全部；仅控制；没有");

            this.ProactiveEvade = menu.Add(
                new MenuToggleKey("Enable proactive evade", Key.None, false).SetTooltip(
                    "Tries to predict and evade instant cast abilities (hex etc.)"));
            this.ProactiveEvade.DisableSave();
            this.ProactiveEvade.AddTranslation(Lang.Ru, "Включить проактив режим");
            this.ProactiveEvade.AddTooltipTranslation(Lang.Ru, "Пытаться уклоняться от мгновенных способностей (хекс и т.д.)");
            this.ProactiveEvade.AddTranslation(Lang.Cn, "主动躲避状态");
            this.ProactiveEvade.AddTooltipTranslation(Lang.Cn, "尝试预测和躲避即时施法能力（羊刀紫苑禁魔变羊等）");

            this.BkbEnabled = menu.Add(new MenuToggleKey("Enable bkb").SetTooltip("Enables/disables black king bar usage with a hotkey"));
            this.BkbEnabled.AddTranslation(Lang.Ru, "Включить бкб");
            this.BkbEnabled.AddTooltipTranslation(Lang.Ru, "Включить/выключить бкб используя клавишу");
            this.BkbEnabled.AddTranslation(Lang.Cn, "启用" + LocalizationHelper.LocalizeName(AbilityId.item_black_king_bar));
            this.BkbEnabled.AddTooltipTranslation(
                Lang.Cn,
                "通过热键启用/禁用" + LocalizationHelper.LocalizeName(AbilityId.item_black_king_bar) + "状态");

            this.OverrideDodgeMode = menu.Add(
                new MenuHoldKey("Override dodge mode").SetTooltip(
                    "While holding the key, you can move your hero when actions are blocked by dodge"));
            this.OverrideDodgeMode.AddTranslation(Lang.Ru, "Отменить режим доджа");
            this.OverrideDodgeMode.AddTooltipTranslation(Lang.Ru, "Держа клавишу, режим доджа не будет блокирывать контроль героя");
            this.OverrideDodgeMode.AddTranslation(Lang.Cn, "覆盖闪躲模式");
            this.OverrideDodgeMode.AddTooltipTranslation(Lang.Cn, "按住键时，当动作被闪避阻止时，您可以移动英雄");

            this.DrawState = menu.Add(new MenuSwitcher("Draw state", false).SetTooltip("Always show hotkeys state"));
            this.DrawState.AddTranslation(Lang.Ru, "Показывать статус");
            this.DrawState.AddTooltipTranslation(Lang.Ru, "Всегда показывать статус клавиш");
            this.DrawState.AddTranslation(Lang.Cn, "图形状态");
            this.DrawState.AddTooltipTranslation(Lang.Cn, "始终显示热键状态");

            rootMenu.Add(menu);
        }

        public MenuToggleKey BkbEnabled { get; }

        public MenuSwitcher DrawState { get; }

        public MenuHoldKey OverrideDodgeMode { get; }

        public MenuHoldKey PathfinderMode { get; }

        public MenuToggleKey ProactiveEvade { get; }
    }
}