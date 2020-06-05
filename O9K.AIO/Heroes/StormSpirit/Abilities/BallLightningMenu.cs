namespace O9K.AIO.Heroes.StormSpirit.Abilities
{
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class BallLightningMenu : UsableAbilityMenu
    {
        public BallLightningMenu(Ability9 ability, string simplifiedName)
            : base(ability, simplifiedName)
        {
            this.Mode = this.Menu.Add(
                new MenuSelector<BallLightning.Mode>("Mode", new[] { BallLightning.Mode.Aggressive, BallLightning.Mode.Defensive }));
            this.Mode.SetTooltip("Aggressive - jump on the target; Defensive - keep distance");
            this.Mode.AddTranslation(Lang.Ru, "Режим");
            this.Mode.AddTooltipTranslation(Lang.Ru, "Агрессивный - лететь на цель; Оборонительный - держать дистанцию");
            this.Mode.AddValuesTranslation(Lang.Ru, new[] { "Агрессивный", "Оборонительный" });
            this.Mode.AddTranslation(Lang.Cn, "模式");
            this.Mode.AddTooltipTranslation(Lang.Cn, "积极 - 飞向目标; 防御 - 保持距离");
            this.Mode.AddValuesTranslation(Lang.Cn, new[] { "积极", "防御" });

            this.MaxCastRange = this.Menu.Add(
                new MenuSlider("Max cast range", ability.DefaultName + "maxRange" + simplifiedName, 1500, 500, 5000));
            this.MaxCastRange.AddTranslation(Lang.Ru, "Максимальная дистанция полета");
            this.MaxCastRange.AddTranslation(Lang.Cn, "最大飞行距离");

            this.MaxDamageCombo = this.Menu.Add(
                new MenuSwitcher("Max damage", ability.DefaultName + "damage" + simplifiedName, false).SetTooltip(
                    "Will spam ultimate for maximum damage output (not recommended with low mana pool)"));
            this.MaxDamageCombo.AddTranslation(Lang.Ru, "Максимальный урон");
            this.MaxDamageCombo.AddTooltipTranslation(
                Lang.Ru,
                "Спам ультой для максимального урона (не рекомендуется при низком запасе маны)");
            this.MaxDamageCombo.AddTranslation(Lang.Cn, "最大伤害");
            this.MaxDamageCombo.AddTooltipTranslation(
                Lang.Cn,
                "垃圾邮件" + LocalizationHelper.LocalizeName(AbilityId.storm_spirit_ball_lightning) + "的最大伤害（不推荐用于低魔法库存");
        }

        public MenuSlider MaxCastRange { get; }

        public MenuSwitcher MaxDamageCombo { get; }

        public MenuSelector<BallLightning.Mode> Mode { get; }
    }
}