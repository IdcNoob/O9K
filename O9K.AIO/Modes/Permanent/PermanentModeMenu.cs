namespace O9K.AIO.Modes.Permanent
{
    using System.Collections.Generic;

    using Base;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class PermanentModeMenu : BaseModeMenu
    {
        private readonly Dictionary<string, string> cnLoc = new Dictionary<string, string>
        {
            {
                "Hold \"w\" to auto cast chains when using fist manually",
                "握住键自动" + LocalizationHelper.LocalizeName(AbilityId.ember_spirit_searing_chains)
            },
            { "Auto use \"X return\"", "自动使用" + LocalizationHelper.LocalizeName(AbilityId.kunkka_return) },
        };

        private readonly Dictionary<string, string> ruLoc = new Dictionary<string, string>
        {
            {
                "Hold \"w\" to auto cast chains when using fist manually",
                "Удерживайте клавишу для авто " + LocalizationHelper.LocalizeName(AbilityId.ember_spirit_searing_chains)
            },
            { "Auto use \"X return\"", "Автоматически использовать " + LocalizationHelper.LocalizeName(AbilityId.kunkka_return) },
        };

        public PermanentModeMenu(Menu rootMenu, string displayName, string tooltip = null)
            : base(rootMenu, displayName)
        {
            this.Enabled = new MenuSwitcher("Enabled", "Enabled" + this.SimplifiedName, true, true);
            this.Enabled.AddTranslation(Lang.Ru, "Включено");
            this.Enabled.AddTranslation(Lang.Cn, "启用");

            if (tooltip != null)
            {
                this.Enabled.SetTooltip(tooltip);

                if (this.ruLoc.TryGetValue(tooltip, out var loc))
                {
                    this.Enabled.AddTooltipTranslation(Lang.Ru, loc);
                }

                if (this.cnLoc.TryGetValue(tooltip, out loc))
                {
                    this.Enabled.AddTooltipTranslation(Lang.Cn, loc);
                }
            }

            this.Menu.Add(this.Enabled);
            rootMenu.Add(this.Menu);
        }

        public MenuSwitcher Enabled { get; }
    }
}