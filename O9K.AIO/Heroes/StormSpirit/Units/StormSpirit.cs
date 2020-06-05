namespace O9K.AIO.Heroes.StormSpirit.Units
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
    using Core.Extensions;
    using Core.Helpers;

    using Ensage;
    using Ensage.SDK.Geometry;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_storm_spirit))]
    internal class StormSpirit : ControllableUnit //, IDisposable
    {
        private readonly Sleeper overloadSleeper = new Sleeper();

        private BallLightning ball;

        private DisableAbility bloodthorn;

        private NukeAbility dagon;

        private DisableAbility hex;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private NukeAbility remnant;

        private DebuffAbility shiva;

        private DisableAbility vortex;

        public StormSpirit(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.storm_spirit_static_remnant, x => this.remnant = new NukeAbility(x) },
                { AbilityId.storm_spirit_electric_vortex, x => this.vortex = new DisableAbility(x) },
                { AbilityId.storm_spirit_ball_lightning, x => this.ball = new BallLightning(x) },

                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_shivas_guard, x => this.shiva = new DebuffAbility(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_dagon_5, x => this.dagon = new NukeAbility(x) },
            };

            this.MoveComboAbilities.Add(AbilityId.storm_spirit_ball_lightning, _ => this.ball);
        }

        public void ChargeOverload()
        {
            if (!this.IsValid || this.overloadSleeper)
            {
                return;
            }

            if (this.Owner.HasModifier("modifier_storm_spirit_overload"))
            {
                return;
            }

            var ult = this.ball?.Ability;
            if (ult?.CanBeCasted() != true)
            {
                return;
            }

            ult.UseAbility(this.Owner.IsMoving ? this.Owner.InFront(100) : this.Owner.InFront(25));
            this.overloadSleeper.Sleep(1);
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
            var target = targetManager.Target;

            if (abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (abilityHelper.CanBeCasted(this.vortex) && (target.CanBecomeMagicImmune || target.CanBecomeInvisible))
            {
                if (abilityHelper.UseAbility(this.vortex))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.nullifier))
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

            var overloaded = this.Owner.CanAttack(target, 25) && this.Owner.HasModifier("modifier_storm_spirit_overload");
            var projectile = ObjectManager.TrackingProjectiles.FirstOrDefault(
                x => x.Source?.Handle == this.Handle && x.Target?.Handle == target.Handle && x.IsAutoAttackProjectile());

            if (overloaded)
            {
                if (projectile == null)
                {
                    return false;
                }

                var distance = target.IsMoving && target.GetAngle(projectile.Position) > 1.5f ? 250 : 350;
                if (projectile.Position.Distance2D(projectile.TargetPosition) > distance)
                {
                    return false;
                }
            }
            else
            {
                if (projectile != null)
                {
                    var overload = this.Owner.Abilities.FirstOrDefault(x => x.Id == AbilityId.storm_spirit_overload);
                    if (overload != null)
                    {
                        var attackDamage = this.Owner.GetAttackDamage(target);
                        var overloadDamage = overload.GetDamage(target);
                        var health = target.Health;

                        if (attackDamage < health && attackDamage + overloadDamage > health)
                        {
                            if (abilityHelper.CanBeCasted(this.remnant, false, false) && abilityHelper.ForceUseAbility(this.remnant, true))
                            {
                                return true;
                            }

                            if (abilityHelper.CanBeCasted(this.ball, false, false))
                            {
                                var distance = projectile.Position.Distance2D(projectile.TargetPosition);
                                var time = distance / projectile.Speed;

                                if (time > this.ball.Ability.CastPoint && abilityHelper.ForceUseAbility(this.ball, true))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            if (abilityHelper.UseAbility(this.vortex))
            {
                this.ComboSleeper.ExtendSleep(0.1f);
                this.remnant?.Sleeper.Sleep(1f);
                this.ball?.Sleeper.Sleep(1f);
                return true;
            }

            if (abilityHelper.UseAbility(this.remnant))
            {
                this.ComboSleeper.ExtendSleep(0.1f);
                this.ball?.Sleeper.Sleep(1f);
                return true;
            }

            if (abilityHelper.UseAbilityIfCondition(this.ball, this.remnant, this.vortex))
            {
                this.ComboSleeper.ExtendSleep(0.3f);
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

            if (abilityHelper.UseMoveAbility(this.ball))
            {
                return true;
            }

            return false;
        }
    }
}