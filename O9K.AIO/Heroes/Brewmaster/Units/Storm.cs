namespace O9K.AIO.Heroes.Brewmaster.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    [UnitName("npc_dota_brewmaster_storm_1")]
    [UnitName("npc_dota_brewmaster_storm_2")]
    [UnitName("npc_dota_brewmaster_storm_3")]
    internal class Storm : ControllableUnit
    {
        private DebuffAbility cender;

        private Cyclone cyclone;

        private AoeAbility dispel;

        private WindWalk windWalk;

        public Storm(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.brewmaster_storm_cyclone, x => this.cyclone = new Cyclone(x) },
                { AbilityId.brewmaster_storm_dispel_magic, x => this.dispel = new Dispel(x) },
                { AbilityId.brewmaster_storm_wind_walk, x => this.windWalk = new WindWalk(x) },
                { AbilityId.brewmaster_cinder_brew, x => this.cender = new DebuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.brewmaster_storm_wind_walk, _ => this.windWalk);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.cender))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.windWalk))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.cyclone))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dispel))
            {
                return true;
            }

            return false;
        }

        public bool CycloneTarget(TargetManager targetManager)
        {
            if (this.cyclone == null)
            {
                return false;
            }

            var ability = this.cyclone.Ability;
            if (!ability.CanBeCasted() || !ability.CanHit(targetManager.Target))
            {
                return false;
            }

            return ability.UseAbility(targetManager.Target);
        }

        protected override bool MoveComboUseBuffs(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBuffs(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.windWalk))
            {
                return true;
            }

            return false;
        }
    }
}