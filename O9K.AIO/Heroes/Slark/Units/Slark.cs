namespace O9K.AIO.Heroes.Slark.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_slark))]
    internal class Slark : ControllableUnit
    {
        private DisableAbility abyssal;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private ShieldAbility dance;

        private DebuffAbility diffusal;

        private DisableAbility hex;

        private DebuffAbility medallion;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private NukeAbility pact;

        private SpeedBuffAbility phase;

        private Pounce pounce;

        private ForceStaff pounceBlink;

        private BuffAbility shadow;

        private BuffAbility silver;

        private DebuffAbility solar;

        public Slark(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.slark_dark_pact, x => this.pact = new NukeAbility(x) },
                { AbilityId.slark_pounce, x => this.pounce = new Pounce(x) },
                { AbilityId.slark_shadow_dance, x => this.dance = new ShadowDance(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_silver_edge, x => this.silver = new BuffAbility(x) },
                { AbilityId.item_invis_sword, x => this.shadow = new BuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.slark_pounce, x => this.pounceBlink = new ForceStaff(x));
        }

        public override bool IsInvisible
        {
            get
            {
                return this.Owner.IsInvisible && !this.Owner.HasModifier("modifier_slark_shadow_dance");
            }
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 400, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                this.pounce?.Sleeper.Sleep(0.3f);
                return true;
            }

            if (abilityHelper.UseAbility(this.pounce))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.medallion))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.solar))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.silver))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shadow))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pact))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dance))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
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

            if (abilityHelper.UseMoveAbility(this.pounceBlink))
            {
                return true;
            }

            return false;
        }
    }
}