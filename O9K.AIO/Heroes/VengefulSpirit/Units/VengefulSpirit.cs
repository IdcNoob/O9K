namespace O9K.AIO.Heroes.VengefulSpirit.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_vengefulspirit))]
    internal class VengefulSpirit : ControllableUnit
    {
        private ForceStaff force;

        private DebuffAbility medallion;

        private DisableAbility missile;

        private HurricanePike pike;

        private DebuffAbility solar;

        private TargetableAbility swap;

        private DebuffAbility urn;

        private DebuffAbility vessel;

        private DebuffAbility wave;

        public VengefulSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.vengefulspirit_magic_missile, x => this.missile = new DisableAbility(x) },
                { AbilityId.vengefulspirit_wave_of_terror, x => this.wave = new DebuffAbility(x) },
                { AbilityId.vengefulspirit_nether_swap, x => this.swap = new TargetableAbility(x) },

                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_hurricane_pike, x => this.pike = new HurricanePike(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.lion_impale, _ => this.missile);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.missile))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.swap))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.vessel))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.urn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.wave))
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

            if (abilityHelper.UseAbility(this.force, 550, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pike, 550, 400))
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

            if (abilityHelper.UseMoveAbility(this.missile))
            {
                return true;
            }

            return false;
        }
    }
}