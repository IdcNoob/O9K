namespace O9K.AIO.Heroes.Juggernaut.Units
{
    using System;
    using System.Collections.Generic;

    using Abililities;

    using Abilities;
    using Abilities.Items;

    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_juggernaut))]
    internal class Juggernaut : ControllableUnit
    {
        private DisableAbility abyssal;

        private NukeAbility bladeFury;

        private ShieldAbility bladeFuryShield;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DebuffAbility diffusal;

        private BuffAbility manta;

        private ShieldAbility mjollnir;

        private Nullifier nullifier;

        private Omnislash omni;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private Omnislash slash;

        private HealingWard ward;

        public Juggernaut(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.juggernaut_blade_fury, x => this.bladeFury = new NukeAbility(x) },
                { AbilityId.juggernaut_healing_ward, x => this.ward = new HealingWard(x) },
                { (AbilityId)419, x => this.slash = new Omnislash(x) },
                { AbilityId.juggernaut_omni_slash, x => this.omni = new Omnislash(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.juggernaut_blade_fury, x => this.bladeFuryShield = new ShieldAbility(x));
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

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

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.omni))
            {
                this.slash?.Sleeper.Sleep(0.2f);
                return true;
            }

            if (abilityHelper.UseAbility(this.slash))
            {
                this.omni?.Sleeper.Sleep(0.2f);
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 400, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ward))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeFury))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.manta, this.Owner.GetAttackRange()))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (target != null && this.Owner.HasModifier("modifier_juggernaut_blade_fury"))
            {
                return this.Move(target.InFront(100));
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseShields(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseShields(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.bladeFuryShield))
            {
                return true;
            }

            return false;
        }
    }
}