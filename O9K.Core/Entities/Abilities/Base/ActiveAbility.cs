namespace O9K.Core.Entities.Abilities.Base
{
    using System.Collections.Generic;

    using Components.Base;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Prediction.Collision;
    using Prediction.Data;

    using SharpDX;

    public abstract class ActiveAbility : Ability9, IActiveAbility
    {
        protected SpecialData SpeedData;

        protected ActiveAbility(Ability baseAbility)
            : base(baseAbility)
        {
            this.TargetsAlly = (baseAbility.TargetTeamType & (TargetTeamType.All | TargetTeamType.Allied)) != TargetTeamType.Enemy;
            this.TargetsEnemy = (baseAbility.TargetTeamType & (TargetTeamType.All | TargetTeamType.Enemy)) != TargetTeamType.Allied;
        }

        public virtual bool BreaksLinkens
        {
            get
            {
                return this.UnitTargetCast;
            }
        }

        public virtual bool CanBeCastedWhileChanneling
        {
            get
            {
                return (this.AbilityBehavior & AbilityBehavior.IgnoreChannel) != 0;
            }
        }

        public virtual CollisionTypes CollisionTypes { get; } = CollisionTypes.None;

        public virtual bool HasAreaOfEffect
        {
            get
            {
                return this.CollisionTypes == CollisionTypes.None && this.Radius > 0;
            }
        }

        public bool IsImmediateCasting
        {
            get
            {
                return (this.AbilityBehavior & AbilityBehavior.Immediate) != 0;
            }
        }

        public bool NoTargetCast
        {
            get
            {
                return (this.AbilityBehavior & AbilityBehavior.NoTarget) != 0;
            }
        }

        public bool PositionCast
        {
            get
            {
                return (this.AbilityBehavior & AbilityBehavior.Point) != 0;
            }
        }

        public virtual SkillShotType SkillShotType { get; } = SkillShotType.None;

        public virtual float Speed
        {
            get
            {
                return this.SpeedData?.GetValue(this.Level) ?? 0;
            }
        }

        public virtual bool TargetsAlly { get; }

        public virtual bool TargetsEnemy { get; }

        public bool UnitTargetCast
        {
            get
            {
                return (this.AbilityBehavior & AbilityBehavior.UnitTarget) != 0;
            }
        }

        protected Sleeper ActionSleeper { get; } = new Sleeper();

        protected bool CanBeCastedWhileRooted
        {
            get
            {
                return (this.AbilityBehavior & AbilityBehavior.RootDisables) == 0;
            }
        }

        protected virtual bool CanBeCastedWhileStunned { get; } = false;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            // todo add random not stun modifiers check (axe taunt etc.)

            if (this.ActionSleeper.IsSleeping)
            {
                return false;
            }

            //if (this.Owner.IsCasting)
            //{
            //    return false;
            //}

            if (!this.IsReady)
            {
                return false;
            }

            if (!this.Owner.IsAlive)
            {
                return false;
            }

            if (this.Owner.IsCharging && !this.IsImmediateCasting)
            {
                return false;
            }

            if (checkChanneling && !this.CanBeCastedWhileChanneling && this.Owner.IsChanneling)
            {
                return false;
            }

            if (!this.CanBeCastedWhileStunned && this.Owner.IsStunned)
            {
                return false;
            }

            if (!this.CanBeCastedWhileRooted && (this.Owner.IsRooted || this.Owner.IsLeashed))
            {
                return false;
            }

            if ((this.IsItem && this.Owner.IsMuted) || (!this.IsItem && this.Owner.IsSilenced))
            {
                return false;
            }

            return true;
        }

        public virtual bool CanHit(Unit9 target)
        {
            return true;
        }

        public virtual bool CanHit(Unit9 mainTarget, List<Unit9> aoeTargets, int minCount)
        {
            return true;
        }

        public virtual float GetCastDelay(Unit9 unit)
        {
            if (this.Owner.Equals(unit))
            {
                return this.GetCastDelay();
            }

            return this.GetCastDelay(unit.Position);
        }

        public virtual float GetCastDelay(Vector3 position)
        {
            if (this.NoTargetCast)
            {
                return this.GetCastDelay();
            }

            return this.GetCastDelay() + this.Owner.GetTurnTime(position);
        }

        public virtual float GetCastDelay()
        {
            return this.CastPoint + InputLag;
        }

        public virtual float GetHitTime(Unit9 unit)
        {
            if (this.Owner.Equals(unit))
            {
                return this.GetCastDelay() + this.ActivationDelay;
            }

            return this.GetHitTime(unit.Position);
        }

        public virtual float GetHitTime(Vector3 position)
        {
            var time = this.GetCastDelay(position) + this.ActivationDelay;

            if (this.Speed > 0)
            {
                return time + (this.Owner.Distance(position) / this.Speed);
            }

            return time;
        }

        public virtual PredictionInput9 GetPredictionInput(Unit9 target, List<Unit9> aoeTargets = null)
        {
            var input = new PredictionInput9
            {
                Caster = this.Owner,
                Target = target,
                CollisionTypes = this.CollisionTypes,
                Delay = this.CastPoint + this.ActivationDelay + InputLag,
                Speed = this.Speed,
                Range = this.Range,
                Radius = this.Radius,
                CastRange = this.CastRange,
                SkillShotType = this.SkillShotType,
                RequiresToTurn = !this.NoTargetCast
            };

            if (aoeTargets != null)
            {
                input.AreaOfEffect = this.HasAreaOfEffect;
                input.AreaOfEffectTargets = aoeTargets;
            }

            return input;
        }

        public virtual PredictionOutput9 GetPredictionOutput(PredictionInput9 input)
        {
            return this.PredictionManager.GetPrediction(input);
        }

        public virtual bool UseAbility(
            Unit9 mainTarget,
            List<Unit9> aoeTargets,
            HitChance minimumChance,
            int minAOETargets = 0,
            bool queue = false,
            bool bypass = false)
        {
            return this.UseAbility(queue, bypass);
        }

        public virtual bool UseAbility(Unit9 target, HitChance minimumChance, bool queue = false, bool bypass = false)
        {
            return this.UseAbility(queue, bypass);
        }

        public virtual bool UseAbility(bool queue = false, bool bypass = false)
        {
            var result = this.BaseAbility.UseAbility(queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }

        public virtual bool UseAbility(Unit9 target, bool queue = false, bool bypass = false)
        {
            bool result;

            if (this.UnitTargetCast)
            {
                result = this.BaseAbility.UseAbility(target.BaseUnit, queue, bypass);
            }
            else if (this.PositionCast)
            {
                result = this.BaseAbility.UseAbility(target.Position, queue, bypass);
            }
            else
            {
                result = this.BaseAbility.UseAbility(queue, bypass);
            }

            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }

        public virtual bool UseAbility(Tree target, bool queue = false, bool bypass = false)
        {
            var result = this.BaseAbility.UseAbility(target, queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }

        public virtual bool UseAbility(Vector3 position, bool queue = false, bool bypass = false)
        {
            bool result;
            if (this.PositionCast)
            {
                result = this.BaseAbility.UseAbility(position, queue, bypass);
            }
            else
            {
                result = this.BaseAbility.UseAbility(queue, bypass);
            }

            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }

        public virtual bool UseAbility(Rune target, bool queue = false, bool bypass = false)
        {
            var result = this.BaseAbility.UseAbility(target, queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
        }
    }
}