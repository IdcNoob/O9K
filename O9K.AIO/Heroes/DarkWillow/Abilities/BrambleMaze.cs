namespace O9K.AIO.Heroes.DarkWillow.Abilities
{
    using System;
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;

    using Ensage.SDK.Geometry;

    using SharpDX;

    using TargetManager;

    internal class BrambleMaze : DisableAbility
    {
        public BrambleMaze(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target);
            var output = this.Ability.GetPredictionOutput(input);
            var targetPosition = output.TargetPosition;
            var castPosition = Vector3.Zero;

            foreach (var vector3 in this.GetMazePositions(targetPosition))
            {
                var tempCast = targetPosition.Extend2D(vector3, -vector3.Distance2D(targetPosition));

                if (this.Owner.Distance(tempCast) > this.Ability.CastRange)
                {
                    continue;
                }

                castPosition = tempCast;
                break;
            }

            if (castPosition.IsZero)
            {
                return false;
            }

            if (!this.Ability.UseAbility(castPosition))
            {
                return false;
            }

            var hitTime = this.Ability.GetHitTime(targetManager.Target) + 0.5f;
            var delay = this.Ability.GetCastDelay(targetManager.Target);

            targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, hitTime);
            comboSleeper.Sleep(delay);
            this.OrbwalkSleeper.Sleep(delay);
            this.Sleeper.Sleep(hitTime);

            return true;
        }

        private IEnumerable<Vector3> GetMazePositions(Vector3 center)
        {
            var list = new List<Vector3>();

            for (var i = 0; i < 2; i++)
            {
                var alpha = (Math.PI / 2) * i;
                var polar = new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);

                var range = polar * 200;

                list.Add(center - range);
                list.Add(center + range);
            }

            for (var i = 0; i < 2; i++)
            {
                var alpha = (Math.PI / 4) + ((Math.PI / 2) * i);
                var polar = new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);

                var range = polar * 500;

                list.Add(center - range);
                list.Add(center + range);
            }

            return list;
        }
    }
}