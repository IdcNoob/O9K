namespace O9K.Farm.Units
{
    using System;
    using System.Linq;

    using Base;

    using Ensage;
    using Ensage.SDK.Helpers;

    using O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities;
    using O9K.Core.Entities.Metadata;
    using O9K.Core.Entities.Units;
    using O9K.Core.Helpers.Damage;
    using O9K.Core.Managers.Menu;
    using O9K.Core.Managers.Menu.Items;

    [UnitName(nameof(HeroId.npc_dota_hero_invoker))]
    internal class Invoker : FarmHero
    {
        private MenuSwitcher UseOrbDamage;

        public Invoker(Unit9 unit)
            : base(unit)
        {
        }

        public override void CreateMenu(Menu unitsMenu)
        {
            base.CreateMenu(unitsMenu);
            this.UseOrbDamage = this.Menu.LastHitMenu.Menu.Add(new MenuSwitcher("Calculate exort damage", false));
            this.UseOrbDamage.AddTranslation(Lang.Ru, "Считать урон сфер " + LocalizationHelper.LocalizeName(AbilityId.invoker_exort));
            this.UseOrbDamage.AddTranslation(Lang.Cn, "计算损坏" + LocalizationHelper.LocalizeName(AbilityId.invoker_exort));
        }

        public override int GetAverageDamage(FarmUnit target)
        {
            return this.Unit.GetAttackDamage(target.Unit, DamageValue.Average, this.OrbsDamage());
        }

        public override int GetDamage(FarmUnit target)
        {
            return this.Unit.GetAttackDamage(target.Unit, DamageValue.Minimum, this.OrbsDamage());
        }

        public override int GetMaxDamage(FarmUnit target)
        {
            return this.Unit.GetAttackDamage(target.Unit, DamageValue.Maximum, this.OrbsDamage());
        }

        private int OrbsDamage()
        {
            if (!this.UseOrbDamage)
            {
                return 0;
            }

            var exort = (Exort)this.Unit.Abilities.FirstOrDefault(x => x.Id == AbilityId.invoker_exort);
            if (exort?.CanBeCasted() != true)
            {
                return 0;
            }

            const int CalculateOrbs = 3;
            const int MaxOrbs = 3;

            var possibleOrbs = MaxOrbs - Math.Min(this.Unit.BaseModifiers.Count(x => x.Name == exort.ModifierName), CalculateOrbs);

            return possibleOrbs * exort.DamagePerOrb;
        }
    }
}