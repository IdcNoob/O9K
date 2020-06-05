namespace O9K.AIO.Heroes.Mars.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_mars))]
    internal class Mars : ControllableUnit
    {
        private DisableAbility abyssal;

        private ArenaOfBlood arena;

        private BuffAbility armlet;

        private ShieldAbility bkb;

        private ShieldAbility bladeMail;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DisableAbility halberd;

        private DisableAbility hex;

        private DebuffAbility medallion;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private NukeAbility rebuke;

        private DebuffAbility solar;

        private SpearOfMars spear;

        public Mars(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.mars_spear, x => this.spear = new SpearOfMars(x) },
                { AbilityId.mars_gods_rebuke, x => this.rebuke = new GodsRebuke(x) },
                { AbilityId.mars_arena_of_blood, x => this.arena = new ArenaOfBlood(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
                { AbilityId.item_armlet, x => this.armlet = new BuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.solar))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.medallion))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 400, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.armlet, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.arena))
            {
                return true;
            }

            if (this.arena?.Sleeper.IsSleeping == true || targetManager.Target.HasModifier("modifier_mars_arena_of_blood_leash"))
            {
                if (abilityHelper.UseAbility(this.rebuke))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.spear))
                {
                    return true;
                }
            }
            else
            {
                if (abilityHelper.UseAbility(this.spear))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.rebuke))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.bladeMail, 400))
            {
                return true;
            }

            return false;
        }
    }
}