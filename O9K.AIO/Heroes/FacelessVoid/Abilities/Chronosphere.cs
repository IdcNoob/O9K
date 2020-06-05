namespace O9K.AIO.Heroes.FacelessVoid.Abilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AIO.Abilities;
    using AIO.Abilities.Menus;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Helpers;
    using Core.Prediction.Data;

    using Ensage.SDK.Geometry;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class Chronosphere : DisableAbility
    {
        public Chronosphere(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (!base.CanHit(targetManager, comboMenu))
            {
                return false;
            }

            if (!this.Ability.CanHit(targetManager.Target, targetManager.EnemyHeroes, this.TargetsToHit(comboMenu)))
            {
                return false;
            }

            return true;
        }

        public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
        {
            return new UsableAbilityHitCountMenu(this.Ability, simplifiedName);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (this.Owner.HasModifier("modifier_faceless_void_time_walk"))
            {
                return false;
            }

            return true;
        }

        public int TargetsToHit(IComboModeMenu comboMenu)
        {
            var menu = comboMenu.GetAbilitySettingsMenu<UsableAbilityHitCountMenu>(this);
            return menu.HitCount;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var input = this.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
            var inputDelay = input.Delay;
            var output = this.Ability.GetPredictionOutput(input);
            var radius = this.Ability.Radius + 25;

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            var castPosition = output.CastPosition;
            var allies = targetManager.AllyHeroes.Where(x => !x.Equals(this.Owner)).ToList();
            var allyPositions = allies.Select(x => x.GetPredictedPosition(inputDelay)).ToList();
            var castPositions = new Dictionary<Vector3, int>
            {
                { castPosition, allyPositions.Count(x => x.Distance2D(castPosition) < radius) }
            };

            if (castPositions[castPosition] > 0)
            {
                // yolo -50fps

                var ownerPosition = this.Owner.Position;
                var lineCount = (int)Math.Ceiling(radius / 50);

                for (var i = 0; i < lineCount; i++)
                {
                    var alpha = (Math.PI / lineCount) * i;
                    var polar = new Vector3((float)Math.Cos(alpha), (float)Math.Sin(alpha), 0);

                    for (var j = 0.25f; j <= 1; j += 0.25f)
                    {
                        var range = polar * (input.CastRange * j);
                        var start = ownerPosition - range;
                        var end = ownerPosition + range;

                        if (output.AoeTargetsHit.All(x => x.TargetPosition.Distance2D(start) < this.Ability.Radius))
                        {
                            castPositions[start] = allyPositions.Count(x => x.Distance2D(start) < radius);
                        }

                        if (output.AoeTargetsHit.All(x => x.TargetPosition.Distance2D(end) < this.Ability.Radius))
                        {
                            castPositions[end] = allyPositions.Count(x => x.Distance2D(end) < radius);
                        }
                    }
                }

                castPosition = castPositions.OrderBy(x => x.Value).ThenBy(x => x.Key.Distance2D(castPosition)).Select(x => x.Key).First();
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

        protected override bool ChainStun(Unit9 target, bool invulnerability)
        {
            return true;
        }
    }
}