namespace O9K.AIO.Heroes.ShadowFiend.Units
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Abilities;
    using Abilities.Items;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using TargetManager;

    [UnitName(nameof(HeroId.npc_dota_hero_nevermore))]
    internal class ShadowFiend : ControllableUnit
    {
        private readonly List<NukeAbility> razes = new List<NukeAbility>();

        private AbilityHelper abilityHelper;

        private ShieldAbility bkb;

        private BlinkAbility blink;

        private DisableAbility bloodthorn;

        private EtherealBlade ethereal;

        private EulsScepterOfDivinity euls;

        private DisableAbility hex;

        private BuffAbility manta;

        private Nullifier nullifier;

        private DisableAbility orchid;

        private HurricanePike pike;

        private bool razeOrbwalk;

        private NukeAbility requiem;

        private DebuffAbility veil;

        public ShadowFiend(Unit9 owner, MultiSleeper abilitySleeper, Sleeper orbwalkSleeper, ControllableUnitMenu menu)
            : base(owner, abilitySleeper, orbwalkSleeper, menu)
        {
            this.ComboAbilities = new Dictionary<AbilityId, Func<ActiveAbility, UsableAbility>>
            {
                {
                    AbilityId.nevermore_shadowraze1, x =>
                        {
                            var raze = new NukeAbility(x);
                            this.razes.Add(raze);
                            return raze;
                        }
                },
                {
                    AbilityId.nevermore_shadowraze2, x =>
                        {
                            var raze = new NukeAbility(x);
                            this.razes.Add(raze);
                            return raze;
                        }
                },
                {
                    AbilityId.nevermore_shadowraze3, x =>
                        {
                            var raze = new NukeAbility(x);
                            this.razes.Add(raze);
                            return raze;
                        }
                },
                { AbilityId.nevermore_requiem, x => this.requiem = new NukeAbility(x) },

                { AbilityId.item_veil_of_discord, x => this.veil = new DebuffAbility(x) },
                { AbilityId.item_orchid, x => this.orchid = new DisableAbility(x) },
                { AbilityId.item_bloodthorn, x => this.bloodthorn = new Bloodthorn(x) },
                { AbilityId.item_nullifier, x => this.nullifier = new Nullifier(x) },
                { AbilityId.item_ethereal_blade, x => this.ethereal = new EtherealBlade(x) },
                { AbilityId.item_sheepstick, x => this.hex = new DisableAbility(x) },
                { AbilityId.item_manta, x => this.manta = new BuffAbility(x) },
                { AbilityId.item_blink, x => this.blink = new BlinkAbility(x) },
                { AbilityId.item_cyclone, x => this.euls = new EulsScepterOfDivinity(x) },
                { AbilityId.item_hurricane_pike, x => this.pike = new HurricanePike(x) },
                { AbilityId.item_black_king_bar, x => this.bkb = new ShieldAbility(x) },
            };
        }

        public override bool Combo(TargetManager targetManager, ComboModeMenu comboModeMenu)
        {
            this.abilityHelper = new AbilityHelper(targetManager, comboModeMenu, this);
            var target = targetManager.Target;
            this.razeOrbwalk = false;

            if (this.UltCombo(targetManager, this.abilityHelper))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.hex))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.bloodthorn))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.orchid))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.veil))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.nullifier))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.ethereal))
            {
                return true;
            }

            if (this.abilityHelper.UseAbility(this.pike, 800, 400))
            {
                return true;
            }

            if (this.abilityHelper.CanBeCasted(this.pike) && !this.MoveSleeper.IsSleeping)
            {
                if (this.pike.UseAbilityOnTarget(targetManager, this.ComboSleeper))
                {
                    return true;
                }
            }

            if (this.abilityHelper.UseAbility(this.manta, this.Owner.GetAttackRange()))
            {
                return true;
            }

            var orderedRazes = target.GetAngle(this.Owner.Position) > 1 || !target.IsMoving
                                   ? this.razes.OrderBy(x => x.Ability.Id)
                                   : this.razes.OrderByDescending(x => x.Ability.Id);

            foreach (var raze in orderedRazes)
            {
                if (!this.abilityHelper.CanBeCasted(raze))
                {
                    continue;
                }

                if (this.RazeCanWaitAttack(raze, target))
                {
                    continue;
                }

                if (this.abilityHelper.UseAbility(raze))
                {
                    return true;
                }
            }

            this.razeOrbwalk = true;
            return false;
        }

        public override bool Orbwalk(Unit9 target, bool attack, bool move, ComboModeMenu comboMenu = null)
        {
            if (this.razeOrbwalk && this.abilityHelper != null && target != null && this.Owner.Speed >= 305
                && (target.IsRanged || target.GetImmobilityDuration() > 1))
            {
                var distance = this.Owner.Distance(target);

                foreach (var raze in this.razes.OrderBy(x => Math.Abs(x.Ability.CastRange - distance)))
                {
                    if (!this.abilityHelper.CanBeCasted(raze, false))
                    {
                        continue;
                    }

                    var position = target.Position.Extend2D(this.Owner.Position, raze.Ability.CastRange - (raze.Ability.Radius - 100));
                    var distance2 = this.Owner.Distance(position);

                    if (target.GetAttackRange(this.Owner) >= distance2 || target.GetImmobilityDuration() > 1)
                    {
                        if (distance2 > 50)
                        {
                            return this.Move(position);
                        }
                    }
                }
            }

            return base.Orbwalk(target, attack, move, comboMenu);
        }

        private bool RazeCanWaitAttack(UsableAbility raze, Unit9 target)
        {
            if (raze.Ability.GetDamage(target) > target.Health)
            {
                return false;
            }

            var input = raze.Ability.GetPredictionInput(target);

            if (this.MoveSleeper.IsSleeping)
            {
                input.Delay += this.MoveSleeper.RemainingSleepTime;
            }
            else if (!target.IsMoving || !this.Owner.CanAttack(target, -50))
            {
                return false;
            }
            else
            {
                input.Delay += this.Owner.GetAttackPoint(target) + this.Owner.GetTurnTime(target.Position);
            }

            var output = raze.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            return true;
        }

        private bool UltCombo(TargetManager targetManager, AbilityHelper abilityHelper)
        {
            if (!abilityHelper.CanBeCasted(this.requiem, false, false))
            {
                return false;
            }

            var target = targetManager.Target;
            var position = target.Position;
            var distance = this.Owner.Distance(position);
            var requiredTime = this.requiem.Ability.CastPoint + (Game.Ping / 2000);
            const float AdditionalTime = 0.3f;

            if (target.IsInvulnerable)
            {
                var time = target.GetInvulnerabilityDuration();
                if (time <= 0)
                {
                    return true;
                }

                var eulsModifier = target.GetModifier("modifier_eul_cyclone");
                if (eulsModifier != null)
                {
                    var particle = eulsModifier.ParticleEffects.FirstOrDefault();
                    if (particle != null)
                    {
                        position = particle.GetControlPoint(0);
                        distance = this.Owner.Distance(position);
                    }
                }

                var remainingTime = time - requiredTime;

                if (remainingTime <= -AdditionalTime)
                {
                    return false;
                }

                if (distance < 100)
                {
                    if (abilityHelper.UseAbility(this.bkb))
                    {
                        return true;
                    }

                    if (remainingTime <= 0 && abilityHelper.ForceUseAbility(this.requiem))
                    {
                        return true;
                    }

                    if (!this.OrbwalkSleeper.IsSleeping)
                    {
                        this.OrbwalkSleeper.Sleep(0.1f);
                        this.Owner.BaseUnit.Move(position);
                    }

                    return true;
                }

                if (distance / this.Owner.Speed < remainingTime + AdditionalTime)
                {
                    if (abilityHelper.UseAbility(this.bkb))
                    {
                        return true;
                    }

                    this.OrbwalkSleeper.Sleep(0.1f);
                    this.ComboSleeper.Sleep(0.1f);
                    return this.Owner.BaseUnit.Move(position);
                }

                if (abilityHelper.CanBeCasted(this.blink))
                {
                    var blinkRange = this.blink.Ability.CastRange + (this.Owner.Speed * remainingTime);
                    if (blinkRange > distance)
                    {
                        if (abilityHelper.UseAbility(this.blink, position))
                        {
                            this.OrbwalkSleeper.Sleep(0.1f);
                            return true;
                        }
                    }
                }
            }

            if (!abilityHelper.CanBeCasted(this.euls, false, false) || !this.euls.ShouldForceCast(targetManager) || target.IsMagicImmune)
            {
                return false;
            }

            var eulsTime = this.euls.Ability.Duration - requiredTime;
            if (abilityHelper.CanBeCasted(this.blink))
            {
                var blinkRange = this.blink.Ability.CastRange + (this.Owner.Speed * eulsTime);
                if (blinkRange > distance)
                {
                    if (abilityHelper.UseAbility(this.blink, position))
                    {
                        this.OrbwalkSleeper.Sleep(0.1f);
                        this.ComboSleeper.ExtendSleep(0.1f);
                        return true;
                    }
                }
            }

            if (distance / this.Owner.Speed < eulsTime)
            {
                if (abilityHelper.UseAbility(this.hex))
                {
                    this.ComboSleeper.ExtendSleep(0.1f);
                    this.OrbwalkSleeper.ExtendSleep(0.1f);
                    return true;
                }

                if (abilityHelper.UseAbility(this.veil))
                {
                    return true;
                }

                if (abilityHelper.UseAbility(this.ethereal))
                {
                    this.OrbwalkSleeper.Sleep(0.5f);
                    return true;
                }

                if (abilityHelper.ForceUseAbility(this.euls))
                {
                    return true;
                }
            }

            return false;
        }
    }
}