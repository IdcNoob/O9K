namespace O9K.AIO.Heroes.ElderTitan.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_elder_titan))]
    internal class ElderTitan : ControllableUnit
    {
        private DisableAbility atos;

        private EulsScepterOfDivinity euls;

        private ForceStaff force;

        private DisableAbility hammer;

        private NukeAbility spirit;

        private NukeAbility splitter;

        private DisableAbility stomp;

        public ElderTitan(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.elder_titan_echo_stomp, x => this.stomp = new DisableAbility(x) },
                { AbilityId.elder_titan_ancestral_spirit, x => this.spirit = new NukeAbility(x) },
                { AbilityId.elder_titan_earth_splitter, x => this.splitter = new NukeAbility(x) },

                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_cyclone, x => this.euls = new EulsScepterOfDivinity(x) },
                { AbilityId.item_meteor_hammer, x => this.hammer = new DisableAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.force, 550, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfAny(this.euls, this.stomp, this.hammer))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.spirit))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.stomp, this.euls))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.hammer, false, false))
            {
                var target = targetManager.Target;
                var immobilityDuration = target.GetImmobilityDuration();

                if (immobilityDuration > this.hammer.Ability.GetHitTime(target) - 0.5f)
                {
                    if (abilityHelper.UseAbility(this.hammer))
                    {
                        return true;
                    }
                }
            }

            if (!abilityHelper.CanBeCasted(this.euls) && !abilityHelper.CanBeCasted(this.stomp, false, false))
            {
                if (abilityHelper.UseAbilityIfNone(this.splitter))
                {
                    return true;
                }
            }

            return false;
        }
    }
}