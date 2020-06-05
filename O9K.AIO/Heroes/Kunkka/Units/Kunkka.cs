namespace O9K.AIO.Heroes.Kunkka.Units
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
    using Core.Logger;
    using Core.Managers.Entity;

    using Ensage;
    using Ensage.SDK.Helpers;

    using SharpDX;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_kunkka))]
    internal class Kunkka : ControllableUnit, IDisposable
    {
        private readonly uint playerHandle;

        private readonly Vector3[] ancientCamps;

        private BuffAbility armlet;

        private BlinkAbility blink;

        private SpeedBuffAbility phase;

        private Ghostship ship;

        private readonly Sleeper stackSleeper = new Sleeper();

        private TargetableAbility tidebringer;

        private Torrent torrent;

        private XMark xMark;

        private UntargetableAbility xReturn;

        public Kunkka(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.playerHandle = owner.BaseOwner.Handle;

            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                { AbilityId.kunkka_torrent, x => this.torrent = new Torrent(x) },
                { AbilityId.kunkka_tidebringer, x => this.tidebringer = new TargetableAbility(x) },
                { AbilityId.kunkka_x_marks_the_spot, x => this.xMark = new XMark(x) },
                { AbilityId.kunkka_return, x => this.xReturn = new UntargetableAbility(x) },
                { AbilityId.kunkka_ghostship, x => this.ship = new Ghostship(x) },

                { AbilityId.item_phase_boots, x => this.phase = new SpeedBuffAbility(x) },
                { AbilityId.item_armlet, x => this.armlet = new BuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
            };

            this.ancientCamps = Bootstrap.Context.JungleManager.JungleCamps.Where(x => x.IsAncient).Select(x => x.CreepsPosition).ToArray();

            Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;

            Unit.OnModifierAdded += this.OnModifierAdded;
            Player.OnExecuteOrder += this.OnExecuteOrder;
        }

        public void AutoReturn()
        {
            if (this.ComboSleeper.IsSleeping)
            {
                return;
            }

            if (this.xMark.Position.IsZero)
            {
                return;
            }

            var xr = this.xReturn.Ability;
            if (!xr.CanBeCasted())
            {
                return;
            }

            if (!this.torrent.ShouldReturn(this.xReturn.Ability, this.xMark.Position)
                && !this.ship.ShouldReturn(this.xReturn.Ability, this.xMark.Position))
            {
                return;
            }

            xr.UseAbility();
            this.ComboSleeper.Sleep(xr.GetCastDelay());
            this.OrbwalkSleeper.Sleep(xr.GetCastDelay());
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            if (comboModeMenu.IsHarassCombo)
            {
                return false;
            }

            var abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);

            if (abilityHelper.CanBeCasted(this.blink) && !abilityHelper.CanBeCasted(this.xReturn))
            {
                var blinkEnemyRange = 0f;

                if (!abilityHelper.CanBeCasted(this.xMark))
                {
                    if (abilityHelper.CanBeCasted(this.xMark, false))
                    {
                        blinkEnemyRange = Math.Min(
                            this.xMark.Ability.CastRange - 100,
                            Math.Max(this.Owner.Distance(targetManager.Target) - this.xMark.Ability.CastRange, 0));
                    }

                    if (abilityHelper.UseAbility(this.blink, 500, blinkEnemyRange))
                    {
                        return true;
                    }
                }
            }

            if (abilityHelper.UseAbilityIfAny(this.xMark, this.torrent, this.ship))
            {
                this.ComboSleeper.ExtendSleep(0.1f);
                this.OrbwalkSleeper.ExtendSleep(0.1f);
                return true;
            }

            if (abilityHelper.CanBeCasted(this.xReturn))
            {
                if (!this.xMark.Position.IsZero)
                {
                    if (abilityHelper.CanBeCasted(this.ship, false))
                    {
                        if (this.ship.UseAbility(this.xMark.Position, targetManager, this.ComboSleeper))
                        {
                            return true;
                        }
                    }

                    if (abilityHelper.CanBeCasted(this.torrent, false))
                    {
                        if (this.torrent.UseAbility(this.xMark.Position, targetManager, this.ComboSleeper))
                        {
                            return true;
                        }
                    }

                    if (this.torrent.ShouldReturn(this.xReturn.Ability, this.xMark.Position)
                        || this.ship?.ShouldReturn(this.xReturn.Ability, this.xMark.Position) == true)
                    {
                        if (abilityHelper.UseAbility(this.xReturn))
                        {
                            return true;
                        }
                    }
                }
            }
            else
            {
                if (abilityHelper.UseAbility(this.torrent))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.ship))
                {
                    return true;
                }
            }

            if (abilityHelper.UseAbility(this.armlet, 400))
            {
                return true;
            }

            if (abilityHelper.UseAbility(this.phase))
            {
                return true;
            }

            return false;
        }

        public void Dispose()
        {
            Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
            Unit.OnModifierAdded -= this.OnModifierAdded;
            Player.OnExecuteOrder -= this.OnExecuteOrder;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (comboMenu != null && target != null)
            {
                if (comboMenu.IsHarassCombo)
                {
                    var ability = this.tidebringer.Ability;
                    if (ability.CanBeCasted())
                    {
                        var ownerPosition = this.Owner.Position;
                        var attackDelay = this.Owner.GetAttackPoint() + (Game.Ping / 1000) + 0.3f;
                        var targetPredictedPosition = target.GetPredictedPosition(attackDelay);

                        var unitTarget = EntityManager9.Units
                            .Where(
                                x => x.IsUnit && !x.Equals(target) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner)
                                     && x.Distance(target) < ability.Range)
                            .OrderBy(x => ownerPosition.AngleBetween(x.Position, targetPredictedPosition))
                            .FirstOrDefault();

                        if (unitTarget != null)
                        {
                            var unitTargetPosition = unitTarget.Position;

                            if (this.CanAttack(unitTarget) && ownerPosition.AngleBetween(unitTargetPosition, targetPredictedPosition) < 45)
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
                    }
                }

                //else
                //{
                //    if (!this.xMark.Position.IsZero && this.xReturn.Ability.CanBeCasted()
                //                                    && this.Owner.Distance(target) > this.Owner.GetAttackRange(target))
                //    {
                //        return this.Move(this.xMark.Position);
                //    }
                //}
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        public void StackAncients()
        {
            if (this.stackSleeper)
            {
                return;
            }

            var ability = this.torrent?.Ability;
            if (ability == null || !ability.CanBeCasted())
            {
                return;
            }

            var camp = this.ancientCamps.OrderBy(x => this.Owner.Distance(x)).First();
            if (this.Owner.Distance(camp) > ability.CastRange)
            {
                return;
            }

            if (Game.GameTime % 60 < 59.5 - ability.GetHitTime(camp))
            {
                return;
            }

            if (ability.UseAbility(camp))
            {
                this.stackSleeper.Sleep(1);
            }
        }

        protected override bool UseOrbAbility(Unit9 target, ComboModeMenu comboMenu)
        {
            if (!this.Owner.CanUseAbilities)
            {
                return false;
            }

            var orb = this.tidebringer.Ability;

            if (comboMenu?.IsAbilityEnabled(orb) != true)
            {
                return false;
            }

            if (orb.CanBeCasted() && orb.CanHit(target) && orb.UseAbility(target))
            {
                return true;
            }

            return false;
        }

        private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
        {
            try
            {
                if (!args.Process || args.OrderId != OrderId.Ability)
                {
                    return;
                }

                if (args.Ability.Handle != this.xReturn.Ability.Handle)
                {
                    return;
                }

                this.torrent.Modifier = null;
                this.xMark.Position = Vector3.Zero;

                if (this.ship != null)
                {
                    this.ship.Position = Vector3.Zero;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
        {
            try
            {
                var modifier = args.Modifier;
                if (!modifier.IsHidden || modifier.Team != this.Owner.Team || modifier.Owner?.Owner?.Handle != this.Owner.Handle)
                {
                    return;
                }

                if (modifier.Name == "modifier_kunkka_torrent_thinker")
                {
                    this.torrent.Modifier = modifier;
                }
            }
            catch (Exception e)
            {
                Logger.Error(e, (EntityManager9.Abilities.Count(x => this.Owner.Equals(x.Owner))).ToString());
            }
        }

        private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
        {
            try
            {
                var particle = args.ParticleEffect;
                if (particle.Owner?.Handle != this.playerHandle)
                {
                    return;
                }

                switch (args.Name)
                {
                    case "particles/units/heroes/hero_kunkka/kunkka_spell_x_spot.vpcf":
                    case "particles/econ/items/kunkka/divine_anchor/hero_kunkka_dafx_skills/kunkka_spell_x_spot_fxset.vpcf":
                    {
                        UpdateManager.BeginInvoke(() => this.xMark.Position = particle.GetControlPoint(0));
                        break;
                    }
                    case "particles/units/heroes/hero_kunkka/kunkka_ghostship_marker.vpcf":
                    {
                        var time = Game.RawGameTime - (Game.Ping / 2000);
                        UpdateManager.BeginInvoke(() => this.ship.CalculateTimings(particle.GetControlPoint(0), time));
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
    }
}