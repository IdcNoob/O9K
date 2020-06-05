namespace O9K.Evader.Pathfinder.Obstacles.Abilities.Proactive
{
    using Core.Entities.Units;

    using O9K.Evader.Abilities.Base.Evadable.Components;

    internal class ProactiveAreaOfEffectObstacle : ProactiveAbilityObstacle
    {
        public ProactiveAreaOfEffectObstacle(IProactiveCounter ability)
            : base(ability)
        {
        }

        public override bool IsIntersecting(Unit9 unit, bool checkPrediction)
        {
            if (!unit.IsMyHero)
            {
                return false;
            }

            var ability = this.EvadableAbility.Ability;

            if (this.Caster.Distance(unit) > ability.Range + 100)
            {
                return false;
            }

            if (!ability.CanBeCasted())
            {
                return false;
            }

            if (unit.IsMagicImmune && !ability.PiercesMagicImmunity(unit))
            {
                return false;
            }

            return true;
        }
    }
}