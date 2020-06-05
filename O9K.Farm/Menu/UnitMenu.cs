namespace O9K.Farm.Menu
{
    using O9K.Core.Entities.Units;
    using O9K.Core.Managers.Menu;
    using O9K.Core.Managers.Menu.Items;

    internal class UnitMenu
    {
        public UnitMenu(Unit9 unit, Menu rootMenu)
        {
            var menu = rootMenu.GetOrAdd(new Menu(unit.DisplayName, unit.Name).SetTexture(unit.DefaultName));

            this.LastHitMenu = new UnitLastHitMenu(unit, menu);
            //  this.UnitPushMenu = new UnitPushMenu(unit, menu);

            this.AdditionalDelay = menu.GetOrAdd(new MenuSlider("Timing adjustment", "delay", 0, -250, 250));
            this.AdditionalDelay.SetTooltip("Higher value => earlier attack");
            this.AdditionalDelay.AddTranslation(Lang.Ru, "Регулировка времени атаки");
            this.AdditionalDelay.AddTooltipTranslation(Lang.Ru, "Чем выше значение, тем раньше начнется атака");
            this.AdditionalDelay.AddTranslation(Lang.Cn, "时间调整");
            this.AdditionalDelay.AddTooltipTranslation(Lang.Cn, "值越高，攻击开始得越快");

            this.BonusRange = menu.GetOrAdd(new MenuSlider("Bonus range", "range", 200, 0, 800));
            this.BonusRange.SetTooltip("Bonus range to attack/check creeps");
            this.BonusRange.AddTranslation(Lang.Ru, "Дополнительная дистанция");
            this.BonusRange.AddTooltipTranslation(Lang.Ru, "Дополнительная дистанция для проверки крипов");
            this.BonusRange.AddTranslation(Lang.Cn, "攻击范围");
            this.BonusRange.AddTooltipTranslation(Lang.Cn, "攻击/检查小兵的奖励范围");
        }

        public MenuSlider AdditionalDelay { get; }

        public MenuSlider BonusRange { get; }

        public UnitLastHitMenu LastHitMenu { get; }
    }

    internal class UnitLastHitMenu
    {
        public UnitLastHitMenu(Unit9 unit, Menu menu)
        {
            this.Menu = menu.GetOrAdd(new Menu("Last hit settings", "lastHit"));
            this.Menu.AddTranslation(Lang.Ru, "Настройки ласт хита");
            this.Menu.AddTranslation(Lang.Cn, "最后一击数值");

            var harassMenu = this.Menu.GetOrAdd(new Menu("Harass", "harass"));
            harassMenu.AddTranslation(Lang.Ru, "Харас");
            harassMenu.AddTranslation(Lang.Cn, "骚扰");

            this.Harass = harassMenu.GetOrAdd(new MenuSwitcher("Enabled", "enabled", unit.IsRanged));
            this.Harass.AddTranslation(Lang.Ru, "Включено");
            this.Harass.AddTranslation(Lang.Cn, "启用");

            this.LastHit = this.Menu.GetOrAdd(new MenuSwitcher("Last hit", "lastHit"));
            this.LastHit.AddTranslation(Lang.Ru, "Ласт хит");
            this.LastHit.AddTranslation(Lang.Cn, "最后一击");

            this.Deny = this.Menu.GetOrAdd(new MenuSwitcher("Deny", "deny"));
            this.Deny.AddTranslation(Lang.Ru, "Денай");
            this.Deny.AddTranslation(Lang.Cn, "反补");

            this.AggressiveDeny = this.Menu.GetOrAdd(
                new MenuSwitcher("Aggressive deny", "aggressive", false).SetTooltip(
                    "Lower chance of actual deny, but harder for enemy to last hit"));
            this.AggressiveDeny.AddTranslation(Lang.Ru, "Агрессивный денай");
            this.AggressiveDeny.AddTooltipTranslation(Lang.Ru, "Шанс деная меньше, но тяжелее ласт хитить для врага");
            this.AggressiveDeny.AddTranslation(Lang.Cn, "进攻性反补");
            this.AggressiveDeny.AddTooltipTranslation(Lang.Cn, "实际反补的机率较低，但对敌人的最后打击更难");
        }

        public MenuSwitcher AggressiveDeny { get; }

        public MenuSwitcher Deny { get; }

        public MenuSwitcher Harass { get; }

        public MenuSwitcher LastHit { get; }

        public Menu Menu { get; }
    }
}