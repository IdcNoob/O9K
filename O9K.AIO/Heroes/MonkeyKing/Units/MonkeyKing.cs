namespace O9K.AIO.Heroes.MonkeyKing.Units
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

    [UnitName(nameof(HeroId.npc_dota_hero_monkey_king))]
    internal class MonkeyKing : ControllableUnit
    {
        private DisableAbility abyssal;

        private WukongsCommand command;

        private DebuffAbility diffusal;

        private SpeedBuffAbility phase;

        private PrimalSpring spring;

        private DisableAbility strike;

        private TreeDance treeDance;

        public MonkeyKing(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.monkey_king_boundless_strike, x => this.strike = new DisableAbility(x) },
                { AbilityId.monkey_king_tree_dance, x => this.treeDance = new TreeDance(x) },
                { AbilityId.monkey_king_primal_spring, x => this.spring = new PrimalSpring(x) },
                { AbilityId.monkey_king_wukongs_command, x => this.command = new WukongsCommand(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_diffusal_blade, x => this.diffusal = new DebuffAbility(x) },
                { AbilityId.item_abyssal_blade, x => this.abyssal = new DisableAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.monkey_king_tree_dance, _ => this.treeDance);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (this.spring.CancelChanneling(targetManager))
            {
                this.ComboSleeper.Sleep(0.1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.abyssal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.diffusal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.spring))
            {
                return true;
            }

            if (abilityHelper.CanBeCastedHidden(this.spring))
            {
                if (abilityHelper.UseAbility(this.treeDance))
                {
                    return true;
                }
            }

            var target = targetManager.Target;
            if (!target.IsRooted && !target.IsStunned && !target.IsHexed)
            {
                if (abilityHelper.UseAbility(this.treeDance, 500, 0))
                {
                    return true;
                }
            }

            var distance = this.Owner.Distance(target);

            if (this.Owner.HasModifier("modifier_monkey_king_quadruple_tap_bonuses")
                || (distance > 600 || (distance > this.Owner.GetAttackRange(target) && target.Speed > this.Owner.Speed
                                                                                    && target.GetImmobilityDuration() <= 0)))
            {
                if (abilityHelper.UseAbility(this.strike))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.command))
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
            if (target != null && this.spring.CanHit(target, comboMenu))
            {
                return false;
            }

            if (this.spring.Ability.IsUsable && !this.treeDance.Ability.IsReady)
            {
                return false;
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool MoveComboUseBlinks(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseBlinks(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.treeDance))
            {
                return true;
            }

            return false;
        }
    }
}