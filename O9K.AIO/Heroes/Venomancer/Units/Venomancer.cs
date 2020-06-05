namespace O9K.AIO.Heroes.Venomancer.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_venomancer))]
    internal class Venomancer : ControllableUnit
    {
        private BlinkDaggerAOE blink;

        private DisableAbility bloodthorn;

        private ForceStaff force;

        private DebuffAbility gale;

        private DebuffAbility nova;

        private DisableAbility orchid;

        private DebuffAbility shiva;

        private DebuffAbility veil;

        private AoeAbility ward;

        public Venomancer(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.venomancer_venomous_gale, x => this.gale = new DebuffAbility(x) },
                { AbilityId.venomancer_plague_ward, x => this.ward = new PlagueWardAbility(x) },
                { AbilityId.venomancer_poison_nova, x => this.nova = new PoisonNova(x) },

                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkDaggerAOE(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.venomancer_venomous_gale, _ => this.gale);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
            var target = targetManager.Target;

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.blink) || this.Owner.Distance(target) < 400)
            {
                if (abilityHelper.UseAbility(this.nova))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.nova))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.nova, false, false))
            {
                if (abilityHelper.UseAbility(this.blink, 500, 300))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.force, 500, 300))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.gale))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ward))
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

            if (abilityHelper.UseAbility(this.gale))
            {
                return true;
            }

            return false;
        }
    }
}