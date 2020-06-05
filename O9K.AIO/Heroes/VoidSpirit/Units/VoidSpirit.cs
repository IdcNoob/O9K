namespace O9K.AIO.Heroes.VoidSpirit.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_void_spirit))]
    internal class VoidSpirit : ControllableUnit
    {
        private readonly Sleeper forceEul = new Sleeper();

        private readonly Sleeper forceRemnant = new Sleeper();

        private ShieldAbility bkb;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private Dissimilate dissimilate;

        private DisableAbility eul;

        private DisableAbility hex;

        private ShieldAbility mjollnir;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private NukeAbility pulse;

        private DisableAbility remnant;

        private DebuffAbility shiva;

        private NukeAbility step;

        private DebuffAbility urn;

        private DebuffAbility veil;

        private DebuffAbility vessel;

        public VoidSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.void_spirit_aether_remnant, x => this.remnant = new AetherRemnant(x) },
                { AbilityId.void_spirit_dissimilate, x => this.dissimilate = new Dissimilate(x) },
                { AbilityId.void_spirit_resonant_pulse, x => this.pulse = new NukeAbility(x) },
                { AbilityId.void_spirit_astral_step, x => this.step = new NukeAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_mjollnir, x => this.mjollnir = new ShieldAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_cyclone, x => this.eul = new DisableAbility(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.Owner.HasModifier("modifier_void_spirit_dissimilate_phase"))
            {
                this.ComboSleeper.Sleep(0.1f);
                this.OrbwalkSleeper.Sleep(0.1f);
                return this.Owner.Move(targetManager.Target.Position);
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bkb, 500))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.mjollnir, 500))
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

            if (abilityHelper.CanBeCasted(this.remnant) && targetManager.Target.GetImmobilityDuration() <= 0)
            {
                if (abilityHelper.CanBeCasted(this.eul, false, false))
                {
                    if (abilityHelper.UseAbility(this.eul) || (this.forceEul && abilityHelper.ForceUseAbility(this.eul)))
                    {
                        this.forceRemnant.Sleep(0.5f);
                        this.forceEul.Reset();
                        return true;
                    }
                }
            }

            if (this.forceRemnant && abilityHelper.CanBeCasted(this.remnant, true, false)
                                  && abilityHelper.ForceUseAbility(this.remnant, true))
            {
                this.forceRemnant.Reset();
                return true;
            }

            if (this.Owner.Distance(targetManager.Target) > 300)
            {
                if (abilityHelper.UseAbility(this.step))
                {
                    this.forceEul.Sleep(0.5f);
                    return true;
                }
            }

            if (!abilityHelper.CanBeCasted(this.step, false) || this.Owner.Distance(targetManager.Target) < 500
                                                             || !targetManager.Target.CanMove())
            {
                if (abilityHelper.UseAbility(this.remnant))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.dissimilate))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.shiva, this.dissimilate))
            {
                return true;
            }

            if (!this.Owner.HasAghanimsScepter || !targetManager.Target.IsSilenced)
            {
                if (abilityHelper.UseAbility(this.pulse))
                {
                    this.pulse.Sleeper.ExtendSleep(0.2f);
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }
    }
}