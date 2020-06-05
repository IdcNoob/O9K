namespace O9K.AIO.Heroes.Pudge.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Items;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;
    using Ensage.SDK.Geometry;

    using TargetManager;

    using Bloodthorn = AIO.Abilities.Items.Bloodthorn;
    using ForceStaff = AIO.Abilities.Items.ForceStaff;
    using Nullifier = AIO.Abilities.Items.Nullifier;

    [UnitName(nameof(HeroId.npc_dota_hero_pudge))]
    internal class Pudge : ControllableUnit
    {
        private DisableAbility atos;

        private ShieldAbility bkb;

        private ShieldAbility bladeMail;

        private BlinkDaggerPudge blink;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private Dismember dismember;

        private DebuffAbility ethereal;

        private ForceStaff force;

        private MeatHook hook;

        private ShieldAbility lotus;

        private Nullifier nullifier;

        private Rot rot;

        private DebuffAbility shiva;

        private DebuffAbility urn;

        private DebuffAbility vessel;

        public Pudge(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.pudge_meat_hook, x => this.hook = new MeatHook(x) },
                { AbilityId.pudge_rot, x => this.rot = new Rot(x) },
                { AbilityId.pudge_dismember, x => this.dismember = new Dismember(x) },

                { AbilityId.item_blink, x => this.blink = new BlinkDaggerPudge(x) },
                { AbilityId.item_force_staff, x => this.force = new ForceStaff(x) },
                { AbilityId.item_blade_mail, x => this.bladeMail = new ShieldAbility(x) },
                { AbilityId.item_spirit_vessel, x => this.vessel = new DebuffAbility(x) },
                { AbilityId.item_urn_of_shadows, x => this.urn = new DebuffAbility(x) },
                { AbilityId.item_rod_of_atos, x => this.atos = new DisableAbility(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new DebuffAbility(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
                { AbilityId.item_lotus_orb, x => this.lotus = new ShieldAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.dagon))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.shiva))
            {
                return true;
            }

            if (targetManager.Target.HasModifier("modifier_pudge_meat_hook"))
            {
                if (abilityHelper.CanBeCasted(this.bloodthorn, true, false))
                {
                    abilityHelper.ForceUseAbility(this.bloodthorn);
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.nullifier, true, false)
                    && !targetManager.Target.Abilities.Any(x => x.Id == AbilityId.item_aeon_disk && x.IsReady))
                {
                    abilityHelper.ForceUseAbility(this.nullifier);
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.ethereal, true, false))
                {
                    abilityHelper.ForceUseAbility(this.ethereal);
                    return true;
                }

                if (abilityHelper.UseAbility(this.bkb))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.lotus))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.dagon))
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
            }

            if (abilityHelper.CanBeCasted(this.dismember))
            {
                if (abilityHelper.CanBeCasted(this.bloodthorn, true, false))
                {
                    abilityHelper.ForceUseAbility(this.bloodthorn);
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.nullifier, true, false)
                    && !targetManager.Target.Abilities.Any(x => x.Id == AbilityId.item_aeon_disk && x.IsReady))
                {
                    abilityHelper.ForceUseAbility(this.nullifier);
                    return true;
                }

                if (abilityHelper.UseAbility(this.bkb))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.lotus))
                {
                    return true;
                }

                if (abilityHelper.CanBeCasted(this.ethereal, true, false))
                {
                    abilityHelper.ForceUseAbility(this.ethereal);
                    return true;
                }

                if (abilityHelper.UseAbility(this.dagon))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.bladeMail))
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

                abilityHelper.ForceUseAbility(this.dismember);
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.blink, this.dismember))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.blink, 800, 25))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.atos))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.force))
            {
                if (this.force.UseAbilityOnTarget(targetManager, this.ComboSleeper))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.force, 400, 800))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.hook))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.rot, false, false))
            {
                if (this.rot.AutoToggle(targetManager))
                {
                    return true;
                }
            }

            return false;
        }

        public void HookAlly(TargetManager targetManager)
        {
            if (this.hook?.Ability.CanBeCasted() != true)
            {
                return;
            }

            var target = targetManager.Target;
            var input = this.hook.Ability.GetPredictionInput(target);
            var output = this.hook.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return;
            }

            this.hook.Ability.UseAbility(output.CastPosition);
        }

        public void SoulRingSuicide(Dictionary<Unit9, float> attacks, Dictionary<TrackingProjectile, int> projectiles)
        {
            if (this.Owner.HealthPercentage > 30)
            {
                return;
            }

            if (this.rot?.Ability.CanBeCasted() != true)
            {
                return;
            }

            var position = this.Owner.IsMoving && Math.Abs(this.Owner.BaseUnit.RotationDifference) < 60
                               ? this.Owner.InFront(55)
                               : this.Owner.Position;

            var noProjectiles = true;
            foreach (var projectile in projectiles)
            {
                if (!projectile.Key.IsValid)
                {
                    continue;
                }

                if (projectile.Key.Position.Distance2D(position) < 200)
                {
                    noProjectiles = false;
                    break;
                }
            }

            var noAutoAttacks = true;
            foreach (var attack in attacks.Where(
                x => x.Key.IsValid && x.Key.IsAlive && x.Key.Distance(this.Owner) <= x.Key.GetAttackRange(this.Owner, 200)
                     && x.Key.GetAngle(this.Owner.Position) < 0.5
                     && (!x.Key.IsRanged || x.Key.Distance(this.Owner) < 400 /*|| x.Key.AttackPoint() < 0.15*/)))
            {
                var unit = attack.Key;
                var attackStart = attack.Value;
                var attackPoint = unit.GetAttackPoint(this.Owner);
                var secondsPerAttack = unit.BaseUnit.SecondsPerAttack;
                var time = Game.RawGameTime;

                var damageTime = attackStart + attackPoint;
                if (unit.IsRanged)
                {
                    damageTime += Math.Max(unit.Distance(this.Owner) - this.Owner.HullRadius, 0) / unit.ProjectileSpeed;
                }

                var echoSabre = unit.Abilities.FirstOrDefault(x => x.Id == AbilityId.item_echo_sabre);

                // fuck calcus
                if ((time <= damageTime // no switch before damage
                     && (attackPoint < 0.35 // no switch if low attackpoint before attack start
                         || time + (attackPoint * 0.6) > damageTime)) // or allow switch if big attack point
                    || (attackPoint < 0.25 // dont allow switch if very low attack point 
                        && time > damageTime + (unit.GetAttackBackswing(this.Owner) * 0.6) // after attack end
                        && time <= attackStart + secondsPerAttack + 0.12) // allow if attack time passed secperatk time
                    || (echoSabre != null && !unit.IsRanged // echo sabre check
                                          && echoSabre.Cooldown - echoSabre.RemainingCooldown <= attackPoint * 2))
                {
                    noAutoAttacks = false;
                    break;
                }
            }

            if (!noProjectiles || !noAutoAttacks)
            {
                return;
            }

            var soulRing = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.item_soul_ring) as SoulRing;
            if (soulRing?.CanBeCasted() == true)
            {
                if (this.Owner.Health > soulRing.HealthCost)
                {
                    return;
                }

                soulRing.UseAbility();

                if (!this.rot.IsEnabled)
                {
                    this.rot.Ability.UseAbility();
                }
            }
            else
            {
                if (this.rot.IsEnabled)
                {
                    return;
                }

                var damage = this.rot.Ability.GetDamage(this.Owner) * 0.5f;

                if (this.Owner.Health > damage)
                {
                    return;
                }

                this.rot.Ability.UseAbility();
            }
        }
    }
}