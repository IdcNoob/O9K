namespace O9K.AIO.Heroes.FacelessVoid.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_faceless_void))]
    internal class FacelessVoid : ControllableUnit
    {
        private ShieldAbility bkb;

        private DisableAbility bloodthorn;

        private Chronosphere chronosphere;

        private DebuffAbility diffusal;

        private TimeDilation dilation;

        private BuffAbility manta;

        private ShieldAbility mjollnir;

        private BuffAbility mom;

        private BuffAbility necro;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private BuffAbility silver;

        private TimeWalk timeWalk;

        public FacelessVoid(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.faceless_void_time_walk, x => this.timeWalk = new TimeWalk(x) },
                { AbilityId.faceless_void_time_dilation, x => this.dilation = new TimeDilation(x) },
                { AbilityId.faceless_void_chronosphere, x => this.chronosphere = new Chronosphere(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_mask_of_madness, x => this.mom = new BuffAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_silver_edge, x => this.silver = new BuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_necronomicon_3, x => this.necro = new BuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.faceless_void_time_walk, _ => this.timeWalk);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 500))
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

            if (!abilityHelper.CanBeCasted(this.timeWalk, false) || this.Owner.Distance(targetManager.Target) < 400)
            {
                if (abilityHelper.UseAbility(this.chronosphere))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.timeWalk, this.chronosphere))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dilation))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.silver))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.manta, 300))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.chronosphere, false, false))
            {
                if (abilityHelper.UseAbilityIfNone(this.mom, this.timeWalk))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.necro, 300))
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

            if (abilityHelper.UseMoveAbility(this.timeWalk))
            {
                return true;
            }

            return false;
        }
    }
}