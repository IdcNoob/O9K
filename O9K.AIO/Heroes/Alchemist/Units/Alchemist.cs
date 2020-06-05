namespace O9K.AIO.Heroes.Alchemist.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_alchemist))]
    internal class Alchemist : ControllableUnit
    {
        private DisableAbility abyssal;

        private DebuffAbility acid;

        private BuffAbility armlet;

        private UnstableConcoction concoction;

        private BuffAbility manta;

        private ShieldAbility mjollnir;

        private BuffAbility rage;

        public Alchemist(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.alchemist_acid_spray, x => this.acid = new DebuffAbility(x) },
                { AbilityId.alchemist_unstable_concoction_throw, x => this.concoction = new UnstableConcoction(x) },
                { AbilityId.alchemist_chemical_rage, x => this.rage = new BuffAbility(x) },

                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_armlet, x => this.armlet = new BuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.armlet, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.concoction))
            {
                return true;
            }

            if (this.concoction?.ThrowAway(targetManager, this.ComboSleeper) == true)
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.acid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.rage, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.manta, 400))
            {
                return true;
            }

            return false;
        }
    }
}