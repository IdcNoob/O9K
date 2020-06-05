namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;

    using Ensage;

    internal class DebuffAbilityGroup : OldAbilityGroup<IDebuff, OldDebuffAbility>
    {
        private readonly List<AbilityId> castOrderDown = new List<AbilityId>
        {
            // first
            AbilityId.leshrac_lightning_storm,
            AbilityId.item_dagon
            // last
        };

        private readonly List<AbilityId> castOrderUp = new List<AbilityId>
        {
            // last
            AbilityId.item_ethereal_blade,
            AbilityId.item_veil_of_discord,
            // fist
        };

        public DebuffAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public bool UseAmplifiers(Unit9 target, ComboModeMenu menu)
        {
            foreach (var ability in this.Abilities)
            {
                if (!(ability.Debuff is IHasDamageAmplify))
                {
                    continue;
                }

                if (!ability.Debuff.IsValid)
                {
                    continue;
                }

                if (!ability.CanBeCasted(target, menu))
                {
                    continue;
                }

                if (ability.Use(target))
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OrderAbilities()
        {
            this.Abilities = this.Abilities.OrderByDescending(x => this.castOrderUp.IndexOf(x.Debuff.Id))
                .ThenBy(x => this.castOrderDown.IndexOf(x.Debuff.Id))
                .ThenBy(x => x.Debuff.CastPoint)
                .ToList();
        }
    }
}