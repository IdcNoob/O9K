namespace O9K.AIO.Heroes.EmberSpirit.Modes
{
    using System.Windows.Input;

    using AIO.Modes.Permanent;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class AutoChainsModeMenu : PermanentModeMenu
    {
        public MenuHoldKey fistKey;

        public AutoChainsModeMenu(Menu rootMenu, string displayName, string tooltip = null)
            : base(rootMenu, displayName, tooltip)
        {
            this.fistKey = this.Menu.Add(new MenuHoldKey("Sleight of Fist key", Key.W).SetTooltip("Set to Dota's Sleight of Fist key"));
            this.fistKey.AddTranslation(Lang.Ru, LocalizationHelper.LocalizeName(AbilityId.ember_spirit_sleight_of_fist) + " клавиша");
            this.fistKey.AddTooltipTranslation(Lang.Ru, "Установить ту же клавишу, что и в доте");
            this.fistKey.AddTranslation(Lang.Cn, LocalizationHelper.LocalizeName(AbilityId.ember_spirit_sleight_of_fist) + "键");
            this.fistKey.AddTooltipTranslation(Lang.Cn, "设置与DotA中的密钥相同的键");
        }
    }
}