namespace O9K.AutoUsage.Abilities.HealthRestore.Unique.AttributeShiftStrengthGain
{
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class AttributeShiftStrengthGainSettings
    {
        public AttributeShiftStrengthGainSettings(Menu settings, IActiveAbility ability)
        {
            var menu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));

            this.AutoBalance = menu.GetOrAdd(new MenuSwitcher("Auto balance"));
            this.AutoBalance.SetTooltip("Auto balance to maintain manually set hp amount");
            this.AutoBalance.AddTranslation(Lang.Ru, "Авто баланс");
            this.AutoBalance.AddTooltipTranslation(Lang.Ru, "Автоматически балансировать здоровье");
            this.AutoBalance.AddTranslation(Lang.Cn, "自动平衡");
            this.AutoBalance.AddTooltipTranslation(Lang.Cn, "自动平衡运行状况");
        }

        public MenuSwitcher AutoBalance { get; }
    }
}