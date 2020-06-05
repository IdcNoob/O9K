namespace O9K.AIO.Heroes.Warlock.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_warlock))]
    internal class Warlock : ControllableUnit
    {
        private DisableAbility atos;

        private DebuffAbility bonds;

        private DisableAbility chaos;

        private ForceStaff force;

        private DisableAbility hex;

        private UntargetableAbility refresher;

        private UntargetableAbility refresherShard;

        private DebuffAbility upheaval;

        private DebuffAbility word;

        public Warlock(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.warlock_fatal_bonds, x => this.bonds = new FatalBonds(x) },
                { AbilityId.warlock_shadow_word, x => this.word = new DebuffAbility(x) },
                { AbilityId.warlock_upheaval, x => this.upheaval = new DebuffAbility(x) },
                { AbilityId.warlock_rain_of_chaos, x => this.chaos = new ChaoticOffering(x) },

                { AbilityId.item_refresher, x => this.refresher = new UntargetableAbility(x) },
                { AbilityId.item_refresher_shard, x => this.refresherShard = new UntargetableAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.chaos))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 600, 500))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bonds))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.word))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.refresher) || abilityHelper.CanBeCasted(this.refresherShard))
            {
                if (abilityHelper.CanBeCasted(this.chaos, true, true, true, false) && !this.chaos.Ability.IsReady)
                {
                    var useRefresher = abilityHelper.CanBeCasted(this.refresherShard) ? this.refresherShard : this.refresher;

                    if (abilityHelper.HasMana(this.chaos, useRefresher))
                    {
                        if (abilityHelper.UseAbility(useRefresher))
                        {
                            this.ComboSleeper.ExtendSleep(0.2f);
                            this.upheaval?.Sleeper.Sleep(0.5f);
                            return true;
                        }
                    }
                }
            }

            if (abilityHelper.UseAbility(this.upheaval))
            {
                return true;
            }

            return false;
        }
    }
}