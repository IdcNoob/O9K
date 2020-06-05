namespace O9K.AIO.Heroes.QueenOfPain.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_queenofpain))]
    internal class QueenOfPain : ControllableUnit
    {
        private ShieldAbility bkb;

        private BlinkQueen blink;

        private DisableAbility bloodthorn;

        private DisableAbility hex;

        private ShieldAbility mjollnir;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private NukeAbility scream;

        private DebuffAbility shadowStrike;

        private DebuffAbility shiva;

        private NukeAbility sonic;

        private DebuffAbility veil;

        public QueenOfPain(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.queenofpain_shadow_strike, x => this.shadowStrike = new DebuffAbility(x) },
                { AbilityId.queenofpain_blink, x => this.blink = new BlinkQueen(x) },
                { AbilityId.queenofpain_scream_of_pain, x => this.scream = new NukeAbility(x) },
                { AbilityId.queenofpain_sonic_wave, x => this.sonic = new SonicWave(x) },

                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.queenofpain_blink, _ => this.blink);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
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

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.scream))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 500))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shadowStrike))
            {
                return true;
            }

            var target = targetManager.Target;

            if (abilityHelper.CanBeCasted(this.shadowStrike) || abilityHelper.CanBeCasted(this.scream)
                                                             || abilityHelper.CanBeCasted(this.sonic) || this.Owner.Distance(target)
                                                             > this.Owner.GetAttackRange(target))
            {
                if (abilityHelper.UseAbility(this.blink))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.sonic))
            {
                return true;
            }

            return false;
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.blink))
            {
                return true;
            }

            return false;
        }
    }
}