namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes
{
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base.Components;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;

    using Ensage;

    internal class NukeAbilityGroup : OldAbilityGroup<INuke, OldNukeAbility>
    {
        private readonly List<AbilityId> castOrder = new List<AbilityId>
        {
            AbilityId.item_dagon,
            AbilityId.item_dagon_2,
            AbilityId.item_dagon_3,
            AbilityId.item_dagon_4,
            AbilityId.item_dagon_5,
            AbilityId.item_ethereal_blade,
        };

        private readonly HashSet<AbilityId> killStealOnly = new HashSet<AbilityId>
        {
            AbilityId.antimage_mana_void,
            AbilityId.axe_culling_blade,
            AbilityId.necrolyte_reapers_scythe,
            AbilityId.zuus_thundergods_wrath,
            AbilityId.item_ethereal_blade,
        };

        public NukeAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public override bool Use(Unit9 target, ComboModeMenu menu, params AbilityId[] except)
        {
            foreach (var ability in this.Abilities)
            {
                if (!ability.Ability.IsValid)
                {
                    continue;
                }

                if (except.Contains(ability.Ability.Id))
                {
                    continue;
                }

                if (!ability.CanBeCasted(target, menu))
                {
                    continue;
                }

                if (this.killStealOnly.Contains(ability.Ability.Id) && ability.Nuke.GetDamage(target) < target.Health)
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
            this.Abilities = this.Abilities.OrderBy(x => x.Ability is IChanneled)
                .ThenByDescending(x => this.castOrder.IndexOf(x.Ability.Id))
                .ThenBy(x => x.Ability.CastPoint)
                .ToList();
        }
    }
}