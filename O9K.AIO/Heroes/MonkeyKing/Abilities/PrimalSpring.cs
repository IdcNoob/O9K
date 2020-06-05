namespace O9K.AIO.Heroes.MonkeyKing.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;

    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    using BasePrimalSpring = Core.Entities.Abilities.Heroes.MonkeyKing.PrimalSpring;

    internal class PrimalSpring : NukeAbility
    {
        private readonly BasePrimalSpring primalSpring;

        private Vector3 castPosition;

        public PrimalSpring(ActiveAbility ability)
            : base(ability)
        {
            this.primalSpring = (BasePrimalSpring)ability;
        }

        public bool CancelChanneling(TargetManager targetManager)
        {
            if (!this.Ability.IsChanneling || !this.Ability.BaseAbility.IsChanneling)
            {
                return false;
            }

            var target = targetManager.Target;
            if (target.IsStunned || target.IsRooted)
            {
                return false;
            }

            var travelDistance = this.Owner.Distance(this.castPosition) / this.Ability.Speed;
            var predictedPolygon = new Polygon.Circle(this.castPosition, this.Ability.Radius - 200);
            var currentPolygon = new Polygon.Circle(this.castPosition, this.Ability.Radius - (travelDistance * target.Speed) - 175);

            var input = this.Ability.GetPredictionInput(target);
            input.Delay = (0.75f - this.Ability.BaseAbility.ChannelTime) + travelDistance;
            var output = this.Ability.GetPredictionOutput(input);

            if ((!predictedPolygon.IsInside(output.TargetPosition) && !currentPolygon.IsInside(target.Position)
                                                                   && target.GetAngle(this.castPosition) > 1.5f)
                || this.primalSpring.GetCurrentDamage(target) > target.Health)
            {
                return this.Owner.BaseUnit.Stop();
            }

            return false;
        }

        public bool CanHit(Unit9 target, IComboModeMenu comboMenu)
        {
            if (comboMenu?.IsAbilityEnabled(this.Ability) != false)
            {
                return false;
            }

            if (!this.Ability.CanBeCasted())
            {
                return false;
            }

            if (!this.Ability.CanHit(target))
            {
                return false;
            }

            if (target.IsReflectingDamage)
            {
                return false;
            }

            var damage = this.Ability.GetDamage(target);
            if (damage <= 0)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                if (!this.ChainStun(target, true))
                {
                    return false;
                }
            }

            if (target.IsRooted && !this.Ability.UnitTargetCast && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
            input.Delay = 0.75f;
            var output = this.Ability.GetPredictionOutput(input);
            this.castPosition = output.CastPosition;

            if (!this.Ability.UseAbility(this.castPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}