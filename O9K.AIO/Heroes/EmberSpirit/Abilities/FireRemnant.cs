namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
    using System;
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using TargetManager;

    using BaseFireRemnant = Core.Entities.Abilities.Heroes.EmberSpirit.FireRemnant;

    internal class FireRemnant : NukeAbility
    {
        private readonly BaseFireRemnant remnant;

        public FireRemnant(ActiveAbility ability)
            : base(ability)
        {
            this.remnant = (BaseFireRemnant)ability;
        }

        public uint Charges
        {
            get
            {
                return this.remnant.Charges;
            }
        }

        public float GetDamage(TargetManager targetManager)
        {
            var target = targetManager.Target;
            var speed = this.remnant.Speed;
            var remnants = EntityManager9.Units.Count(
                x => x.IsUnit && x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant"
                     && x.Distance(target.GetPredictedPosition(this.Owner.Distance(x) / speed)) < this.Ability.Radius);

            return this.remnant.GetDamage(target) * (this.remnant.Charges + remnants);
        }

        public float GetRequiredRemnants(TargetManager targetManager)
        {
            var target = targetManager.Target;
            var speed = this.remnant.Speed;
            var damage = this.remnant.GetDamage(target);
            if (damage <= 0)
            {
                return 0;
            }

            var remnants = EntityManager9.Units.Count(
                x => x.IsUnit && x.IsAlly(this.Owner) && x.Name == "npc_dota_ember_spirit_remnant"
                     && x.Distance(target.GetPredictedPosition(this.Owner.Distance(x) / speed)) < this.Ability.Radius);

            var health = target.Health - (remnants * damage);
            var count = 0;

            while (health > 0)
            {
                health -= damage;
                count++;
            }

            return Math.Min(this.Charges, count);
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var input = this.Ability.GetPredictionInput(targetManager.Target);
            input.Delay += (this.Owner.Distance(targetManager.Target) / this.Ability.Speed);
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
            var hitTime = this.Ability.GetHitTime(targetManager.Target);
            this.Sleeper.Sleep(hitTime * 0.6f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}