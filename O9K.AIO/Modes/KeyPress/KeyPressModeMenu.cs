namespace O9K.AIO.Modes.KeyPress
{
    using System.Collections.Generic;

    using Base;

    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class KeyPressModeMenu : BaseModeMenu
    {
        private readonly Dictionary<string, string> cnLoc = new Dictionary<string, string>
        {
            { "Toss enemy to ally tower", LocalizationHelper.LocalizeName(AbilityId.tiny_toss) + "敌人到防御塔" },
            {
                "Use ball lighting to charge overload",
                "使用" + LocalizationHelper.LocalizeName(AbilityId.storm_spirit_ball_lightning) + "对于"
                + LocalizationHelper.LocalizeName(AbilityId.storm_spirit_overload)
            },
        };

        private readonly Dictionary<string, string> ruLoc = new Dictionary<string, string>
        {
            { "Toss enemy to ally tower", "Тосс врага к союзной вышке" },
            { "Use ball lighting to charge overload", "Использовать ульту для заряда оверлорда" },
        };

        public KeyPressModeMenu(Menu rootMenu, string displayName, string tooltip = null)
            : base(rootMenu, displayName)
        {
            this.Key = new MenuHoldKey("Key", "key" + this.SimplifiedName, System.Windows.Input.Key.None, true);
            this.Key.AddTranslation(Lang.Ru, "Клавиша");
            this.Key.AddTranslation(Lang.Cn, "键");

            if (tooltip != null)
            {
                this.Key.SetTooltip(tooltip);

                if (this.ruLoc.TryGetValue(tooltip, out var loc))
                {
                    this.Key.AddTooltipTranslation(Lang.Ru, loc);
                }

                if (this.cnLoc.TryGetValue(tooltip, out loc))
                {
                    this.Key.AddTooltipTranslation(Lang.Cn, loc);
                }
            }

            this.Menu.Add(this.Key);
            rootMenu.Add(this.Menu);
        }

        public MenuHoldKey Key { get; }
    }
}