namespace O9K.AIO.Heroes.Enigma.Abilities
{
    using System;
    using System.Collections.Generic;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;

    using Modes.Combo;

    using SharpDX;

    using TargetManager;

    internal class BlinkDaggerEnigma : BlinkAbility
    {
        private Vector3 blinkPosition;

        public BlinkDaggerEnigma(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
        {
            var blackHole = (BlackHole)usableAbilities.Find(x => x.Ability.Id == AbilityId.enigma_black_hole);
            if (blackHole == null)
            {
                return false;
            }

            var input = blackHole.Ability.GetPredictionInput(targetManager.Target, EntityManager9.EnemyHeroes);
            input.CastRange += this.Ability.CastRange;
            input.Range += this.Ability.CastRange;
            var output = blackHole.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low || output.AoeTargetsHit.Count < blackHole.TargetsToHit(menu))
            {
                return false;
            }

            var range = Math.Min(this.Ability.CastRange, this.Owner.Distance(output.CastPosition) - 200);
            this.blinkPosition = this.Owner.Position.Extend2D(output.CastPosition, range);

            if (this.Owner.Distance(this.blinkPosition) > this.Ability.CastRange)
            {
                return false;
            }

            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            if (!this.Ability.UseAbility(this.blinkPosition))
            {
                return false;
            }

            //comboSleeper.Sleep(0.3f);
            this.OrbwalkSleeper.Sleep(0.5f);
            this.Sleeper.Sleep(0.5f);
            return true;
        }
    }
}