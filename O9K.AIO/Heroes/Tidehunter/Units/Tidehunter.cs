namespace O9K.AIO.Heroes.Tidehunter.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_tidehunter))]
    internal class Tidehunter : ControllableUnit
    {
        private ShieldAbility bladeMail;

        private BlinkDaggerAOE blink;

        private ForceStaff force;

        private NukeAbility gush;

        private Ravage ravage;

        private UntargetableAbility refresher;

        private UntargetableAbility refresherShard;

        private DebuffAbility shiva;

        private NukeAbility smash;

        public Tidehunter(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.tidehunter_gush, x => this.gush = new NukeAbility(x) },
                { AbilityId.tidehunter_anchor_smash, x => this.smash = new NukeAbility(x) },
                { AbilityId.tidehunter_ravage, x => this.ravage = new Ravage(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkDaggerAOE(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_refresher, x => this.refresher = new UntargetableAbility(x) },
                { AbilityId.item_refresher_shard, x => this.refresherShard = new UntargetableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
            var target = targetManager.Target;

            if (!abilityHelper.CanBeCasted(this.blink) || this.Owner.Distance(target) < 400)
            {
                if (abilityHelper.UseAbility(this.ravage))
                {
                    return true;
                }
            }

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.ravage))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.ravage, false, false))
            {
                if (abilityHelper.UseAbility(this.blink, 400, 0))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.force, 400, 0))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.smash))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.refresher) || abilityHelper.CanBeCasted(this.refresherShard))
            {
                if (abilityHelper.CanBeCasted(this.ravage, true, true, true, false) && !this.ravage.Ability.IsReady)
                {
                    var useRefresher = abilityHelper.CanBeCasted(this.refresherShard) ? this.refresherShard : this.refresher;

                    if (abilityHelper.HasMana(this.ravage, useRefresher))
                    {
                        if (abilityHelper.UseAbility(useRefresher))
                        {
                            return true;
                        }
                    }
                }
            }

            if (abilityHelper.UseAbility(this.gush))
            {
                return true;
            }

            return false;
        }
    }
}