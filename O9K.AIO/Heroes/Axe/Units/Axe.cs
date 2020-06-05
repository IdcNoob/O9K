namespace O9K.AIO.Heroes.Axe.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_axe))]
    internal class Axe : ControllableUnit
    {
        private ShieldAbility bkb;

        private CullingBlade blade;

        private ShieldAbility bladeMail;

        private BlinkDaggerAOE blink;

        private DisableAbility call;

        private NukeAbility dagon;

        private ForceStaff force;

        private DebuffAbility hunger;

        private DisableAbility meteor;

        private ShieldAbility mjollnir;

        private DebuffAbility shiva;

        public Axe(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.axe_berserkers_call, x => this.call = new DisableAbility(x) },
                { AbilityId.axe_battle_hunger, x => this.hunger = new DebuffAbility(x) },
                { AbilityId.axe_culling_blade, x => this.blade = new CullingBlade(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkDaggerAOE(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
                { AbilityId.item_meteor_hammer, x => this.meteor = new MeteorHammerAxe(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.axe_berserkers_call, _ => this.call);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blade))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.blade, false) && !abilityHelper.CanBeCasted(this.blade))
            {
                if (abilityHelper.UseAbility(this.blink, 200, 0))
                {
                    return true;
                }
            }

            if (abilityHelper.UseDoubleBlinkCombo(this.force, this.blink))
            {
                return true;
            }

            if (abilityHelper.CanBeCastedIfCondition(this.blink, this.call))
            {
                if (abilityHelper.UseAbility(this.bkb))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.call))
            {
                this.call.Sleeper.ExtendSleep(1);
                return true;
            }

            if (abilityHelper.CanBeCasted(this.meteor))
            {
                if (targetManager.Target.HasModifier("modifier_axe_berserkers_call") && abilityHelper.UseAbility(this.meteor))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.call))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 0))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.call, false))
            {
                if (abilityHelper.UseAbility(this.bladeMail, 400))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.mjollnir, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.hunger))
            {
                if (abilityHelper.CanBeCasted(this.call))
                {
                    return false;
                }

                if (abilityHelper.CanBeCasted(this.meteor) && this.call?.Sleeper.IsSleeping == true)
                {
                    return false;
                }

                if (abilityHelper.UseAbility(this.hunger))
                {
                    return true;
                }
            }

            return false;
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.call))
            {
                return true;
            }

            return false;
        }
    }
}