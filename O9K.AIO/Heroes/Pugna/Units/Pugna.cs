namespace O9K.AIO.Heroes.Pugna.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;
    using Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_pugna))]
    internal class Pugna : ControllableUnit
    {
        private DisableAbility atos;

        private NukeAbility blast;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private DebuffAbility decrepify;

        private TargetableAbility drain;

        private DisableAbility hex;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private DebuffAbility veil;

        private AoeAbility ward;

        public Pugna(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.pugna_nether_blast, x => this.blast = new NukeAbility(x) },
                { AbilityId.pugna_decrepify, x => this.decrepify = new DebuffAbility(x) },
                { AbilityId.pugna_nether_ward, x => this.ward = new AoeAbility(x) },
                { AbilityId.pugna_life_drain, x => this.drain = new TargetableAbility(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.drain?.Ability.IsChanneling == true)
            {
                if (this.dagon?.Ability.CanBeCasted(false) == true && this.dagon.Ability.CanHit(targetManager.Target)
                                                                   && this.dagon.Ability.GetDamage(targetManager.Target)
                                                                   > targetManager.Target.Health)
                {
                    this.Owner.Stop();
                    this.ComboSleeper.Sleep(0.1f);
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.blink, 700, 350))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
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

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.decrepify))
            {
                this.ComboSleeper.ExtendSleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blast))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ward))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.drain))
            {
                return true;
            }

            return false;
        }
    }
}