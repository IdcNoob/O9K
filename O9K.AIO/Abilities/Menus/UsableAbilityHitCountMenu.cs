namespace O9K.AIO.Abilities.Menus
{
    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    internal class UsableAbilityHitCountMenu : UsableAbilityMenu
    {
        public UsableAbilityHitCountMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.HitCount = this.Menu.Add(
                new MenuSlider("Enemy count", ability.DefaultName + "hitCount" + simplifiedName, 2, 1, 5).SetTooltip(
                    "Use ability only if it will hit equals/more enemies"));
            this.HitCount.AddTranslation(Lang.Ru, "Количество врагов");
            this.HitCount.AddTooltipTranslation(Lang.Ru, "Использовать способность, только если она поразит больше врагов");
            this.HitCount.AddTranslation(Lang.Cn, "敌人计数");
            this.HitCount.AddTooltipTranslation(Lang.Cn, "仅当它击中更/相等多敌人时，才使用该能力");
        }

        public MenuSlider HitCount { get; }
    }
}