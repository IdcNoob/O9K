namespace O9K.AIO.Heroes.SpiritBreaker.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

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

    [UnitName(nameof(HeroId.npc_dota_hero_spirit_breaker))]
    internal class SpiritBreaker : ControllableUnit
    {
        private ShieldAbility bladeMail;

        private DisableAbility bloodthorn;

        private SpeedBuffAbility bulldoze;

        private ChargeOfDarkness charge;

        private DisableAbility halberd;

        private BuffAbility mom;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private NukeAbility strike;

        private DebuffAbility urn;

        private DebuffAbility vessel;

        public SpiritBreaker(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.spirit_breaker_charge_of_darkness, x => this.charge = new ChargeOfDarkness(x) },
                { AbilityId.spirit_breaker_bulldoze, x => this.bulldoze = new SpeedBuffAbility(x) },
                { AbilityId.spirit_breaker_nether_strike, x => this.strike = new NukeAbility(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_mask_of_madness, x => this.mom = new BuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_heavens_halberd, x => this.halberd = new DisableAbility(x) },
            };
        }

        public void ChargeAway(TargetManager targetManager)
        {
            if (this.charge?.Ability.CanBeCasted() != true)
            {
                return;
            }

            var target = targetManager.EnemyUnits.OrderBy(x => x.IsHero)
                .ThenByDescending(x => x.Distance(this.Owner))
                .FirstOrDefault(x => x.Distance(this.Owner) > 2000);

            if (target == null)
            {
                return;
            }

            this.charge.Ability.UseAbility(target);
            this.OrbwalkSleeper.Sleep(0.5f);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (this.Owner.IsCharging)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

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

            if (abilityHelper.UseAbility(this.halberd))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.charge))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bulldoze, 500))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.bladeMail, 400))
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

            if (abilityHelper.UseAbility(this.strike))
            {
                return true;
            }

            if (abilityHelper.UseAbilityIfNone(this.mom, this.charge, this.bulldoze, this.strike))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (this.Owner.IsCharging)
            {
                return false;
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }
    }
}