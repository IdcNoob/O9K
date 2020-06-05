namespace O9K.Evader.Abilities.Heroes.Morphling.AttributeShiftStrengthGain
{
    using System.Collections.Generic;

    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Heroes.Morphling;
    using Core.Entities.Units;
    using Core.Extensions;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class AttributeShiftStrengthGainUsable : CounterAbility
    {
        private readonly AttributeShiftAgilityGain attributeShiftAgi;

        private readonly AttributeShiftStrengthGain attributeShiftStr;

        private readonly HashSet<AbilityId> forceUse = new HashSet<AbilityId>
        {
            AbilityId.faceless_void_chronosphere,
            AbilityId.enigma_black_hole,
            AbilityId.doom_bringer_doom
        };

        public AttributeShiftStrengthGainUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            this.attributeShiftStr = (AttributeShiftStrengthGain)ability;
            this.attributeShiftAgi = this.attributeShiftStr.AttributeShiftAgilityGain;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (this.attributeShiftStr.Enabled)
            {
                return false;
            }

            if (!ally.CanBeHealed)
            {
                return false;
            }

            if (ally.Agility <= 1)
            {
                return false;
            }

            if (ally.Health > 2000)
            {
                if (this.forceUse.Contains(obstacle.EvadableAbility.Ability.Id))
                {
                    return true;
                }

                if (this.attributeShiftAgi.Enabled)
                {
                    return true;
                }

                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var ability = obstacle.EvadableAbility.Ability;
            var damage = obstacle.GetDamage(ally);
            var remainingTime = obstacle.GetEvadeTime(ally, false) - 0.03f;

            if (obstacle.IsModifierObstacle)
            {
                return this.ActiveAbility.GetHitTime(ally);
            }

            if (damage > ally.Health)
            {
                var requiredHealth = damage - ally.Health;
                if (this.attributeShiftStr.HealthGain(remainingTime) < requiredHealth)
                {
                    return 9999;
                }

                return remainingTime;
            }

            if (ability.IsDisable())
            {
                return remainingTime;
            }

            return 9999;
        }
    }
}