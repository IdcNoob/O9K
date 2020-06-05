namespace O9K.AIO.Heroes.TrollWarlord.Units
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

    using SharpDX;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_troll_warlord))]
    internal class TrollWarlord : ControllableUnit
    {
        private DisableAbility abyssal;

        private NukeAbility axeMelee;

        private NukeAbility axeRanged;

        private ShieldAbility bkb;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private DebuffAbility diffusal;

        private DisableAbility hex;

        private BuffAbility mom;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private UntargetableAbility rage;

        private ShieldAbility trance;

        public TrollWarlord(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.troll_warlord_berserkers_rage, x => this.rage = new UntargetableAbility(x) },
                { AbilityId.troll_warlord_whirling_axes_melee, x => this.axeMelee = new NukeAbility(x) },
                { AbilityId.troll_warlord_whirling_axes_ranged, x => this.axeRanged = new NukeAbility(x) },
                { AbilityId.troll_warlord_battle_trance, x => this.trance = new ShieldAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_mask_of_madness, x => this.mom = new BuffAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bkb, 500))
            {
                return true;
            }

            var target = targetManager.Target;
            var distance = this.Owner.Distance(target);
            var attackRange = this.Owner.GetAttackRange(target);

            if (distance < 225 || distance > 575)
            {
                if (this.Owner.IsRanged && abilityHelper.UseAbility(this.rage))
                {
                    return true;
                }
            }
            else
            {
                if (!this.Owner.IsRanged && abilityHelper.UseAbility(this.rage))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.blink, 550, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
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

            if (abilityHelper.UseAbility(this.axeMelee))
            {
                return true;
            }

            if (!this.Owner.IsRanged && distance > 200 && abilityHelper.CanBeCastedHidden(this.axeRanged))
            {
                if (this.axeRanged.CanHit(targetManager, comboModeMenu) && abilityHelper.UseAbility(this.rage))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.axeRanged))
            {
                return true;
            }

            if (this.Owner.HealthPercentage < 20 && abilityHelper.UseAbility(this.trance, attackRange))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            if (!this.Owner.IsRanged)
            {
                if (abilityHelper.UseAbilityIfNone(this.mom, this.axeMelee, this.trance))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (this.OrbwalkSleeper.IsSleeping)
            {
                return false;
            }

            this.LastTarget = target;

            if (attack && this.CanAttack(target))
            {
                this.LastMovePosition = Vector3.Zero;
                return this.Attack(target, comboMenu);
            }

            if (target != null && this.Menu.OrbwalkerStopOnStanding && !this.Owner.IsRanged && !target.IsMoving
                && this.Owner.Distance(target) < this.Owner.GetAttackRange(target))
            {
                return false;
            }

            if (move && this.CanMove())
            {
                return this.ForceMove(target, attack);
            }

            return false;
        }
    }
}