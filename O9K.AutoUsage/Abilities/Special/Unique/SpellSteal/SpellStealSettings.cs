namespace O9K.AutoUsage.Abilities.Special.Unique.SpellSteal
{
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Managers.Menu;
    using Core.Managers.Menu.Items;

    using Ensage;
    using Ensage.SDK.Helpers;

    internal class SpellStealSettings
    {
        private readonly Menu abilityMenu;

        public SpellStealSettings(Menu settings, IActiveAbility ability)
        {
            this.abilityMenu = settings.GetOrAdd(new Menu(ability.DisplayName, ability.Name).SetTexture(ability.Name));
        }

        public Dictionary<string, MenuAbilityToggler> HighPriorityTogglers { get; } = new Dictionary<string, MenuAbilityToggler>();

        public Dictionary<string, MenuAbilityToggler> ScepterTogglers { get; } = new Dictionary<string, MenuAbilityToggler>();

        public Dictionary<string, MenuAbilityToggler> Togglers { get; } = new Dictionary<string, MenuAbilityToggler>();

        public void Add(Ability9 ability)
        {
            var enabled = ability is IDisable || ability.IsUltimate;
            this.GetAbilityToggler(ability).AddAbility(ability.Id, enabled);

            this.GetHighPriorityAbilityToggler(ability).AddAbility(ability.Id);

            this.GetScepterAbilityToggler(ability).AddAbility(ability.Id);
        }

        public bool IsAbilityEnabled(ActiveAbility ability)
        {
            return this.GetAbilityToggler(ability).IsEnabled(ability.Name);
        }

        public bool IsHighPriorityAbilityEnabled(ActiveAbility ability)
        {
            return this.GetHighPriorityAbilityToggler(ability).IsEnabled(ability.Name);
        }

        public bool IsScepterAbilityEnabled(ActiveAbility ability)
        {
            return this.GetScepterAbilityToggler(ability).IsEnabled(ability.Name);
        }

        private MenuAbilityToggler GetAbilityToggler(Ability9 ability)
        {
            if (!this.Togglers.TryGetValue(ability.Owner.Name, out var toggler))
            {
                var heroMenu = this.abilityMenu.GetOrAdd(new Menu(ability.Owner.DisplayName, ability.Owner.Name))
                    .SetTexture(ability.Owner.Name);
                toggler = heroMenu.GetOrAdd(new MenuAbilityToggler("Default steal", "Steal:"))
                    .SetTooltip("These abilities will be stolen only if current one is on cooldown");
                toggler.AddTranslation(Lang.Ru, "Стандартный стилл");
                toggler.AddTooltipTranslation(Lang.Ru, "Эти способности будут украдены, если нынешние в кд");
                toggler.AddTranslation(Lang.Cn, "默认窃取");
                toggler.AddTooltipTranslation(Lang.Cn, "这些技能只有在当前技能冷却时才会被盗");

                this.Togglers.Add(ability.Owner.Name, toggler);
            }

            return toggler;
        }

        private MenuAbilityToggler GetHighPriorityAbilityToggler(Ability9 ability)
        {
            if (!this.HighPriorityTogglers.TryGetValue(ability.Owner.Name, out var toggler))
            {
                var heroMenu = this.abilityMenu.GetOrAdd(new Menu(ability.Owner.DisplayName, ability.Owner.Name))
                    .SetTexture(ability.Owner.Name);
                toggler = heroMenu.GetOrAdd(new MenuAbilityToggler("Priority steal", "highSteal", null, false))
                    .SetTooltip("These abilities will always be stolen (ignores current stolen ability)");
                toggler.AddTranslation(Lang.Ru, "Приоритетный стилл");
                toggler.AddTooltipTranslation(Lang.Ru, "Эти способности всегда будут украдены, игнорируя нынешние");
                toggler.AddTranslation(Lang.Cn, "优先窃取");
                toggler.AddTooltipTranslation(Lang.Cn, "这些能力将始终被盗，而忽略了当前");

                this.HighPriorityTogglers.Add(ability.Owner.Name, toggler);
            }

            return toggler;
        }

        private MenuAbilityToggler GetScepterAbilityToggler(Ability9 ability)
        {
            if (!this.ScepterTogglers.TryGetValue(ability.Owner.Name, out var toggler))
            {
                var heroMenu = this.abilityMenu.GetOrAdd(new Menu(ability.Owner.DisplayName, ability.Owner.Name))
                    .SetTexture(ability.Owner.Name);
                toggler = heroMenu.GetOrAdd(new MenuAbilityToggler("Scepter steal", "stealScepter", null, false))
                    .SetTooltip("These abilities will be stolen only if you have Aghanims Scepter");
                toggler.AddTranslation(Lang.Ru, "Аганимный стилл");
                toggler.AddTooltipTranslation(
                    Lang.Ru,
                    "Эти способности будут украдены, только если у вас есть "
                    + LocalizationHelper.LocalizeName(AbilityId.item_ultimate_scepter));
                toggler.AddTranslation(Lang.Cn, LocalizationHelper.LocalizeName(AbilityId.item_ultimate_scepter) + "窃取");
                toggler.AddTooltipTranslation(Lang.Cn, "这些能力将被盗，如果你有" + LocalizationHelper.LocalizeName(AbilityId.item_ultimate_scepter));

                this.ScepterTogglers.Add(ability.Owner.Name, toggler);
            }

            return toggler;
        }
    }
}