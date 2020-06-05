namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
    using System.Linq;

    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;
    using Core.Extensions;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using SharpDX;

    using TargetManager;

    internal class RollingBoulder : NukeAbility
    {
        public RollingBoulder(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            return true;
        }

        public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
        {
            var target = targetManager.Target;
            var distance = this.Owner.Distance(target);
            var input = this.Ability.GetPredictionInput(target);

            if (EntityManager9.Units.Any(
                x => x.Name == "npc_dota_earth_spirit_stone" && x.Distance(this.Owner) < distance && x.Distance(this.Owner) < 350
                     && x.IsAlive && this.Owner.Position.AngleBetween(x.Position, target.Position) < 75))
            {
                input.Speed *= 2;
                input.CastRange *= 2;
                input.Range *= 2;
            }

            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(targetManager.Target);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public bool SimpleUseAbility(Vector3 position)
        {
            if (!this.Ability.UseAbility(position))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(position);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var distance = this.Owner.Distance(target);
            var input = this.Ability.GetPredictionInput(target);

            if (EntityManager9.Units.Any(
                x => x.Name == "npc_dota_earth_spirit_stone" && x.Distance(this.Owner) < distance && x.Distance(this.Owner) < 350
                     && x.IsAlive && this.Owner.Position.AngleBetween(x.Position, target.Position) < 75))
            {
                input.Speed *= 2;
                input.CastRange *= 2;
                input.Range *= 2;
            }
            else if (distance > 350)
            {
                return false;
            }

            var output = this.Ability.GetPredictionOutput(input);

            if (output.HitChance < HitChance.Low)
            {
                return false;
            }

            if (!this.Ability.UseAbility(output.CastPosition))
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