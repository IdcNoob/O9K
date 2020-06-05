namespace O9K.AIO.Heroes.ShadowShaman.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_shadow_shaman))]
    internal class ShadowShaman : ControllableUnit
    {
        private BlinkAbility blink;

        private EulsScepterOfDivinity euls;

        private ForceStaff force;

        private DisableAbility hex;

        private DisableAbility shackles;

        private NukeAbility shock;

        private Wards wards;

        public ShadowShaman(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.shadow_shaman_ether_shock, x => this.shock = new NukeAbility(x) },
                { AbilityId.shadow_shaman_voodoo, x => this.hex = new DisableAbility(x) },
                { AbilityId.shadow_shaman_shackles, x => this.shackles = new DisableAbility(x) },
                { AbilityId.shadow_shaman_mass_serpent_ward, x => this.wards = new Wards(x) },

                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_cyclone, x => this.euls = new EulsScepterOfDivinity(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.shadow_shaman_voodoo, _ => this.hex);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.wards, true, false))
            {
                if (abilityHelper.UseAbility(this.euls))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.wards))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shackles))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 500, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shock))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            return false;
        }
    }
}