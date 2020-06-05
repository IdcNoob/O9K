namespace O9K.AIO.Heroes.EmberSpirit.Units
{
    using System;
    using System.Collections.Generic;

    using Abilities;

    using AIO.Abilities;
    using AIO.Abilities.Items;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_ember_spirit))]
    internal class EmberSpirit : ControllableUnit
    {
        private ShieldAbility bladeMail;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DisableAbility chains;

        private NukeAbility fist;

        private DisableAbility hex;

        private ShieldAbility mjollnir;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private FireRemnant remnant;

        private ActivateFireRemnant remnantActivate;

        private ActivateFireRemnantBlink remnantActivateBlink;

        private FireRemnantBlink remnantBlink;

        private ShieldAbility shield;

        private DebuffAbility shiva;

        private DebuffAbility veil;

        public EmberSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.ember_spirit_searing_chains, x => this.chains = new SearingChains(x) },
                { AbilityId.ember_spirit_sleight_of_fist, x => this.fist = new SleightOfFist(x) },
                { AbilityId.ember_spirit_flame_guard, x => this.shield = new ShieldAbility(x) },
                { AbilityId.ember_spirit_fire_remnant, x => this.remnant = new FireRemnant(x) },
                { AbilityId.ember_spirit_activate_fire_remnant, x => this.remnantActivate = new ActivateFireRemnant(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.ember_spirit_fire_remnant, x => this.remnantBlink = new FireRemnantBlink(x));
            this.MoveComboAbilities.Add(
                AbilityId.ember_spirit_activate_fire_remnant,
                x => this.remnantActivateBlink = new ActivateFireRemnantBlink(x));
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.hex))
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

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.chains))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.blink, false) && abilityHelper.CanBeCasted(this.chains, false)
                                                             && abilityHelper.CanBeCasted(this.fist, false))
            {
                var fistRange = this.fist.Ability.Range;
                var range = this.blink.Ability.Range + fistRange;
                var target = targetManager.Target;
                var distance = this.Owner.Distance(target);

                if (range > distance && distance > fistRange)
                {
                    if (abilityHelper.UseAbility(this.blink, target.Position))
                    {
                        return true;
                    }
                }
            }

            if (!this.Owner.IsInvulnerable && abilityHelper.UseAbility(this.blink, 700, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.fist))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.remnantActivate, false))
            {
                if (!this.remnant.Sleeper.IsSleeping)
                {
                    if (abilityHelper.ForceUseAbility(this.remnantActivate, true))
                    {
                        return true;
                    }
                }

                //if (abilityHelper.UseAbility(this.remnantActivate))
                //{
                //    return true;
                //}
            }

            if (abilityHelper.CanBeCasted(this.remnant) && abilityHelper.HasMana(this.remnantActivate) && !this.Owner.IsInvulnerable)
            {
                var target = targetManager.Target;
                var distance = target.Distance(this.Owner);

                if (target.GetImmobilityDuration() > 0.8 || distance < 400)
                {
                    var damage = this.remnant.GetDamage(targetManager);
                    if (damage + 100 > target.Health)
                    {
                        for (var i = 0; i < this.remnant.GetRequiredRemnants(targetManager); i++)
                        {
                            abilityHelper.ForceUseAbility(this.remnant);
                        }

                        this.ComboSleeper.Sleep((this.Owner.Distance(target) / this.remnant.Ability.Speed) * 0.6f);
                        return true;
                    }
                }

                if (distance > 350 && abilityHelper.UseAbility(this.remnant))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shield, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 600))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 500))
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

            if (!this.remnantBlink.Sleeper.IsSleeping && abilityHelper.UseMoveAbility(this.remnantActivateBlink))
            {
                this.remnantActivateBlink.Sleeper.Sleep(1f);
                return true;
            }

            if (!this.Owner.IsInvulnerable)
            {
                if (abilityHelper.UseMoveAbility(this.remnantBlink))
                {
                    return true;
                }
            }

            return false;
        }
    }
}