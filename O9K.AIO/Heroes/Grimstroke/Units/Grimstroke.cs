namespace O9K.AIO.Heroes.Grimstroke.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_grimstroke))]
    internal class Grimstroke : ControllableUnit
    {
        private DisableAbility atos;

        private Soulbind bind;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private DisableAbility embrace;

        private EtherealBlade ethereal;

        private DisableAbility halberd;

        private DisableAbility hex;

        private ShieldAbility ink;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private TargetableAbility portrait;

        private DebuffAbility shiva;

        private DebuffAbility stroke;

        private DebuffAbility veil;

        public Grimstroke(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.grimstroke_dark_artistry, x => this.stroke = new DebuffAbility(x) },
                { AbilityId.grimstroke_ink_creature, x => this.embrace = new DisableAbility(x) },
                { AbilityId.grimstroke_spirit_walk, x => this.ink = new InkSwell(x) },
                { AbilityId.grimstroke_soul_chain, x => this.bind = new Soulbind(x) },
                { (AbilityId)7852, x => this.portrait = new TargetableAbility(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new GrimstrokeDagon(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 700, 450))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bind))
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

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.embrace))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.portrait))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ink))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.stroke))
            {
                return true;
            }

            return false;
        }
    }
}