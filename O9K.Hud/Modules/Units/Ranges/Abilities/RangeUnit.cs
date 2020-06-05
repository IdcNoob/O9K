namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
    using System;
    using System.Collections.Generic;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Menu;
    using Core.Managers.Menu.EventArgs;
    using Core.Managers.Menu.Items;

    internal class RangeUnit : IDisposable
    {
        private readonly List<IRange> abilities = new List<IRange>();

        private readonly Menu heroMenu;

        private readonly MenuAbilityToggler toggler;

        private readonly Unit9 unit;

        public RangeUnit(Unit9 unit, Menu menu)
        {
            this.unit = unit;
            this.Handle = unit.Handle;

            this.heroMenu = menu.GetOrAdd(new Menu(unit.DisplayName, unit.DefaultName + unit.IsMyHero)).SetTexture(unit.DefaultName);
            var abilitiesMenu = this.heroMenu.GetOrAdd(new Menu("Enabled", "abilities"));
            abilitiesMenu.AddTranslation(Lang.Ru, "Включено");
            abilitiesMenu.AddTranslation(Lang.Cn, "启用");

            this.toggler = abilitiesMenu.GetOrAdd(new MenuAbilityToggler(string.Empty, "enabled", null, false));
            this.toggler.ValueChange += this.TogglerOnValueChange;

            this.AddCustomRanges();
        }

        public uint Handle { get; }

        public void AddAbility(Ability9 ability)
        {
            var rangeAbility = new AbilityRange(ability);
            this.abilities.Add(rangeAbility);

            if (!this.toggler.AllAbilities.TryGetValue(ability.Name, out var enabled))
            {
                this.toggler.AddAbility(ability.Name);
            }
            else if (enabled)
            {
                rangeAbility.Enable(this.heroMenu);
            }
        }

        public void Dispose()
        {
            foreach (var ability in this.abilities)
            {
                ability.Dispose();
            }

            this.toggler.ValueChange -= this.TogglerOnValueChange;
        }

        public void RemoveAbility(Ability9 ability)
        {
            var rangeAbility = this.abilities.Find(x => x.Handle == ability.Handle);
            if (rangeAbility == null)
            {
                return;
            }

            rangeAbility.Dispose();
            this.abilities.Remove(rangeAbility);
        }

        public void UpdateRanges()
        {
            foreach (var ability in this.abilities)
            {
                if (!ability.IsEnabled)
                {
                    continue;
                }

                ability.UpdateRange();
            }
        }

        private void AddCustomRanges()
        {
            var attackRange = new AttackRange(this.unit);
            this.abilities.Add(attackRange);
            this.toggler.AddAbility("o9k.attack");

            var expRange = new ExperienceRange(this.unit);
            this.abilities.Add(expRange);
            this.toggler.AddAbility("o9k.exp_plus");
        }

        private void TogglerOnValueChange(object sender, AbilityEventArgs e)
        {
            var rangeAbility = this.abilities.Find(x => x.Name == e.Ability);
            if (rangeAbility == null)
            {
                return;
            }

            if (e.NewValue)
            {
                rangeAbility.Enable(this.heroMenu);
            }
            else
            {
                rangeAbility.Dispose();
            }
        }
    }
}