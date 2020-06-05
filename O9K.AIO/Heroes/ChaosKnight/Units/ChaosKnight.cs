namespace O9K.AIO.Heroes.ChaosKnight.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_chaos_knight))]
    internal class ChaosKnight : ControllableUnit
    {
        private BuffAbility armlet;

        private ShieldAbility bkb;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DisableAbility bolt;

        private DisableAbility halberd;

        private BuffAbility manta;

        private DisableAbility orchid;

        private UntargetableAbility phantasm;

        private TargetableAbility rift;

        public ChaosKnight(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.chaos_knight_chaos_bolt, x => this.bolt = new DisableAbility(x) },
                { AbilityId.chaos_knight_reality_rift, x => this.rift = new TargetableAbility(x) },
                { AbilityId.chaos_knight_phantasm, x => this.phantasm = new UntargetableAbility(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_armlet, x => this.armlet = new BuffAbility(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.slardar_sprint, _ => this.bolt);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.armlet, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bkb, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bolt))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.rift))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 500, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.phantasm))
            {
                if (abilityHelper.UseAbility(this.armlet))
                {
                    this.ComboSleeper.ExtendSleep(0.5f);
                    return true;
                }

                if (abilityHelper.UseAbility(this.phantasm))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.manta, 600))
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

            if (abilityHelper.UseAbility(this.bolt))
            {
                return true;
            }

            return false;
        }
    }
}