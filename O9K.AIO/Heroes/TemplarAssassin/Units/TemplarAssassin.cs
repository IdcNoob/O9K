namespace O9K.AIO.Heroes.TemplarAssassin.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.TemplarAssassin;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_templar_assassin))]
    internal class TemplarAssassin : ControllableUnit
    {
        private readonly Sleeper preventAttackSleeper = new Sleeper();

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private ForceStaff force;

        private DisableAbility hex;

        private DebuffAbility medallion;

        private NukeAbility meld;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private HurricanePike pike;

        private BuffAbility refraction;

        private DebuffAbility solar;

        private DebuffAbility trap;

        public TemplarAssassin(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.templar_assassin_refraction, x => this.refraction = new BuffAbility(x) },
                { AbilityId.templar_assassin_meld, x => this.meld = new NukeAbility(x) },
                { AbilityId.templar_assassin_psionic_trap, x => this.trap = new Abilities.PsionicTrap(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_hurricane_pike, x => this.pike = new HurricanePike(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_solar_crest, x => this.solar = new DebuffAbility(x) },
                { AbilityId.item_medallion_of_courage, x => this.medallion = new DebuffAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.templar_assassin_refraction, _ => this.refraction);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (comboModeMenu.IsHarassCombo)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.refraction, 1300))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 500, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.force, 500, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.pike, 500, 0))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.solar))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.medallion))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.meld, false))
            {
                this.preventAttackSleeper.Sleep(0.05f);

                if (!this.AttackSleeper.IsSleeping && abilityHelper.UseAbility(this.meld))
                {
                    var attackDelay = this.Owner.GetAttackPoint() + 0.1f;
                    this.ComboSleeper.ExtendSleep(attackDelay);
                    this.MoveSleeper.Sleep(attackDelay);
                    this.AttackSleeper.Sleep(attackDelay);
                    return true;
                }
            }

            //if (abilityHelper.CanBeCasted(this.pike) && !MoveSleeper.Sleeping)
            //{
            //    if (this.pike.UseAbilityOnTarget(targetManager, ComboSleeper))
            //    {
            //        return true;
            //    }
            //}

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

            if (!abilityHelper.CanBeCasted(this.blink) || this.Owner.Distance(targetManager.Target) < 1000)
            {
                if (abilityHelper.UseAbility(this.trap))
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

            if (target != null && comboMenu?.IsHarassCombo == true && target.Distance(this.Owner) > this.Owner.GetAttackRange(target))
            {
                if (this.MoveSleeper.IsSleeping)
                {
                    return false;
                }

                if (this.MoveToProjectile(target))
                {
                    return true;
                }

                var psi = (PsiBlades)this.Owner.Abilities.First(x => x.Id == AbilityId.templar_assassin_psi_blades);
                var ownerPosition = this.Owner.Position;
                var attackDelay = this.Owner.GetAttackPoint() + (Game.Ping / 1000) + 0.3f;
                var targetPredictedPosition = target.GetPredictedPosition(attackDelay);

                var unitTarget = EntityManager9.Units
                    .Where(
                        x => x.IsUnit && !x.Equals(target) && x.IsAlive && x.IsVisible && !x.IsInvulnerable
                             && (!x.IsAlly(this.Owner) || (x.IsCreep && x.HealthPercentage < 50))
                             && x.Distance(target) < psi.SplitRange - 75)
                    .OrderBy(x => ownerPosition.AngleBetween(x.Position, targetPredictedPosition))
                    .FirstOrDefault();

                if (unitTarget != null)
                {
                    var unitTargetPosition = unitTarget.Position;

                    if (this.CanAttack(unitTarget) && ownerPosition.AngleBetween(unitTargetPosition, targetPredictedPosition) < 15)
                    {
                        this.LastMovePosition = Vector3.Zero;
                        this.LastTarget = unitTarget;
                        this.OrbwalkSleeper.Sleep(0.05f);
                        return this.Attack(unitTarget, comboMenu);
                    }

                    var range = Math.Min(Math.Max(unitTarget.Distance(ownerPosition), 150), this.Owner.GetAttackRange());
                    var movePosition = unitTargetPosition.Extend2D(targetPredictedPosition, -range);
                    this.OrbwalkSleeper.Sleep(0.05f);

                    return this.Move(movePosition);
                }

                attack = false;
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        protected override bool Attack(Unit9 target, ComboModeMenu comboMenu)
        {
            if (this.preventAttackSleeper.IsSleeping)
            {
                return false;
            }

            return base.Attack(target, comboMenu);
        }

        protected override bool MoveComboUseShields(AbilityHelper abilityHelper)
        {
            if (base.MoveComboUseShields(abilityHelper))
            {
                return true;
            }

            if (abilityHelper.UseMoveAbility(this.refraction))
            {
                return true;
            }

            return false;
        }

        private bool MoveToProjectile(Unit9 target)
        {
            if (!this.CanMove())
            {
                return false;
            }

            var projectile = ObjectManager.TrackingProjectiles.FirstOrDefault(
                x => x.IsValid && x.Source?.Handle == this.Owner.Handle && x.Target?.IsValid == true);

            if (projectile == null)
            {
                return false;
            }

            var projectileTarget = projectile.Target;
            var targetPredictedPosition = target.GetPredictedPosition(
                (projectile.Position.Distance2D(projectileTarget.Position) / projectile.Speed) + (Game.Ping / 1000));
            var psiPosition = projectileTarget.Position.Extend2D(targetPredictedPosition, -this.Owner.Distance(projectileTarget.Position));
            this.Owner.BaseUnit.Move(psiPosition);
            return true;
        }
    }
}