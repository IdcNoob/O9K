namespace O9K.AIO.Heroes.Tiny.Units
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
    using Core.Managers.Entity;

    using Ensage;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_tiny))]
    internal class Tiny : ControllableUnit
    {
        private DisableAbility avalanche;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private EtherealBlade ethereal;

        private DisableAbility orchid;

        private SpeedBuffAbility phase;

        private NukeAbility toss;

        private TreeGrab treeGrab;

        private TreeThrow treeThrow;

        private DebuffAbility veil;

        private TreeVolley volley;

        public Tiny(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.tiny_avalanche, x => this.avalanche = new DisableAbility(x) },
                { AbilityId.tiny_toss, x => this.toss = new Toss(x) },
                { AbilityId.tiny_tree_grab, x => this.treeGrab = new TreeGrab(x) },
                { AbilityId.tiny_toss_tree, x => this.treeThrow = new TreeThrow(x) },
                { AbilityId.tiny_tree_channel, x => this.volley = new TreeVolley(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.leshrac_split_earth, _ => this.avalanche);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.blink, 400, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.veil))
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

            if (abilityHelper.UseAbility(this.avalanche))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.toss))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (!abilityHelper.CanBeCasted(this.avalanche, false) && abilityHelper.UseAbility(this.volley))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.treeGrab))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.treeThrow))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        public void Toss()
        {
            var tossAbility = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.tiny_toss) as ActiveAbility;
            if (tossAbility?.CanBeCasted() != true)
            {
                return;
            }

            var tower = EntityManager9.Units.Where(x => x.IsTower && x.IsAlly(this.Owner) && x.IsAlive)
                .OrderBy(x => x.Distance(this.Owner))
                .FirstOrDefault(x => x.Distance(this.Owner) < 2000);
            if (tower == null)
            {
                return;
            }

            var tossTarget = EntityManager9.Units
                .Where(
                    x => x.IsUnit && !x.IsInvulnerable && !x.IsMagicImmune && x.IsAlive && x.IsVisible
                         && x.Distance(this.Owner) < tossAbility.CastRange && x.Distance(tower) < tower.GetAttackRange())
                .OrderBy(x => x.Distance(tower))
                .FirstOrDefault();
            if (tossTarget == null)
            {
                return;
            }

            var grabUnit = EntityManager9.Units
                .Where(
                    x => x.IsUnit && !x.Equals(this.Owner) && !x.IsInvulnerable && !x.IsMagicImmune && x.IsAlive && x.IsVisible
                         && x.Distance(this.Owner) < tossAbility.Radius)
                .OrderBy(x => x.Distance(this.Owner))
                .FirstOrDefault();

            if (grabUnit?.IsHero != true || grabUnit.IsIllusion || grabUnit.IsAlly(this.Owner))
            {
                return;
            }

            tossAbility.UseAbility(tossTarget);
        }

        protected override bool MoveComboUseDisables(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseDisables(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.avalanche))
            {
                return true;
            }

            return false;
        }
    }
}