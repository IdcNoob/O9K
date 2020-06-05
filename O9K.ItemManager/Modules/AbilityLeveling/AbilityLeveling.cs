namespace O9K.ItemManager.Modules.AbilityLeveling
{
    using System;
    using System.ComponentModel.Composition;
    using System.Linq;
    using System.Threading.Tasks;

    using AbilityBuild;

    using Core.Entities.Heroes;
    using Core.Logger;
    using Core.Managers.Entity;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    internal class AbilityLeveling : IModule
    {
        private readonly MenuSelector<BuildType> abilitiesType;

        private readonly MenuSwitcher enabled;

        private readonly MenuSwitcher learnAbilities;

        private readonly MenuSwitcher learnTalents;

        private readonly MenuSelector<BuildType> talentsType;

        private AbilityBuilder abilityBuilder;

        private Owner owner;

        [ImportingConstructor]
        public AbilityLeveling(IMainMenu mainMenu)
        {
            this.enabled = mainMenu.AbilityLevelingMenu.Add(new MenuSwitcher("Enabled", false, true)).SetTooltip("Auto ability leveling");
            this.enabled.AddTranslation(Lang.Ru, "Включено");
            this.enabled.AddTooltipTranslation(Lang.Ru, "Автоматически улучшать способности");
            this.enabled.AddTranslation(Lang.Cn, "启用");
            this.enabled.AddTooltipTranslation(Lang.Cn, "自动技能加点");

            var abilities = mainMenu.AbilityLevelingMenu.Add(new Menu("Abilities"));
            abilities.AddTranslation(Lang.Ru, "Способности");
            abilities.AddTranslation(Lang.Cn, "技能");

            this.learnAbilities = abilities.Add(new MenuSwitcher("Learn", true, true));
            this.learnAbilities.AddTranslation(Lang.Ru, "Улучшать");
            this.learnAbilities.AddTranslation(Lang.Cn, "提高");

            this.abilitiesType =
                abilities.Add(new MenuSelector<BuildType>("Type", Enum.GetValues(typeof(BuildType)).Cast<BuildType>(), true));
            this.abilitiesType.SetTooltip("Note that win rate build might sometimes be bad");
            this.abilitiesType.AddTranslation(Lang.Ru, "Тип улучшений");
            this.abilitiesType.AddTooltipTranslation(Lang.Ru, "Улучшения по количеству побед могут быть плохими");
            this.abilitiesType.AddValuesTranslation(Lang.Ru, new[] { "Количество использований", "Количество побед" });
            this.abilitiesType.AddTranslation(Lang.Cn, "种类");
            this.abilitiesType.AddTooltipTranslation(Lang.Cn, "请注意，胜率构建有时可能是错的");
            this.abilitiesType.AddValuesTranslation(Lang.Cn, new[] { "使用数量", "获胜次数" });

            var talents = mainMenu.AbilityLevelingMenu.Add(new Menu("Talents"));
            talents.AddTranslation(Lang.Ru, "Таланты");
            talents.AddTranslation(Lang.Cn, "天赋树");

            this.learnTalents = talents.Add(new MenuSwitcher("Learn", true, true));
            this.learnTalents.AddTranslation(Lang.Ru, "Улучшать");
            this.learnTalents.AddTranslation(Lang.Cn, "提高");

            this.talentsType = talents.Add(new MenuSelector<BuildType>("Type", Enum.GetValues(typeof(BuildType)).Cast<BuildType>(), true));
            this.talentsType.AddTranslation(Lang.Ru, "Тип улучшений");
            this.talentsType.AddValuesTranslation(Lang.Ru, new[] { "Количество использований", "Количество побед" });
            this.talentsType.AddTranslation(Lang.Cn, "种类");
            this.talentsType.AddValuesTranslation(Lang.Cn, new[] { "使用数量", "获胜次数" });
        }

        public void Activate()
        {
            this.owner = EntityManager9.Owner;
            this.abilityBuilder = new AbilityBuilder(
                this.owner,
                this.learnAbilities,
                this.abilitiesType,
                this.learnTalents,
                this.talentsType);

            this.enabled.ValueChange += this.EnabledOnValueChange;
            this.abilityBuilder.BuildReady += this.OnBuildReady;
        }

        public void Dispose()
        {
            this.enabled.ValueChange -= this.EnabledOnValueChange;
            this.abilityBuilder.BuildReady -= this.OnBuildReady;
            this.learnTalents.ValueChange -= this.LearnOnValueChange;
            this.learnAbilities.ValueChange -= this.LearnOnValueChange;
            Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
        }

        private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue)
            {
                UpdateManager.BeginInvoke(() => this.abilityBuilder.GetBuild(), 3000);
            }

            Entity.OnInt32PropertyChange -= this.OnInt32PropertyChange;
            this.learnTalents.ValueChange -= this.LearnOnValueChange;
            this.learnAbilities.ValueChange -= this.LearnOnValueChange;
        }

        private void LearnAbility(int points)
        {
            try
            {
                var ability = this.abilityBuilder.GetLearnableAbility(points);
                ability?.UpgradeAbility();
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void LearnOnValueChange(object sender, SwitcherEventArgs e)
        {
            if (e.NewValue == e.OldValue || !e.NewValue)
            {
                return;
            }

            UpdateManager.BeginInvoke(() => this.LearnAbility((int)this.owner.Hero.BaseHero.AbilityPoints), 100);
        }

        private void OnBuildReady(object sender, EventArgs eventArgs)
        {
            //var drawings = new BuildDrawer(abilityBuilder, this.abilitiesType, this.talentsType);
            Entity.OnInt32PropertyChange += this.OnInt32PropertyChange;
            this.learnTalents.ValueChange += this.LearnOnValueChange;
            this.learnAbilities.ValueChange += this.LearnOnValueChange;
            this.LearnAbility((int)this.owner.Hero.BaseHero.AbilityPoints);
        }

        private async void OnInt32PropertyChange(Entity sender, Int32PropertyChangeEventArgs args)
        {
            if (sender.Handle != this.owner.HeroHandle || args.OldValue == args.NewValue || args.PropertyName != "m_iAbilityPoints")
            {
                return;
            }

            await Task.Delay(100);
            this.LearnAbility(args.NewValue);
        }
    }
}