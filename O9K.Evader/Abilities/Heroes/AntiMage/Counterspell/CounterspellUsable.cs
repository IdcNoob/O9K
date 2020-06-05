namespace O9K.Evader.Abilities.Heroes.AntiMage.Counterspell
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class CounterspellUsable : CounterAbility
    {
        public CounterspellUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var time = base.GetRequiredTime(ally, enemy, obstacle);
            var ability = obstacle.EvadableAbility.Ability;

            if (ability.Id == AbilityId.lina_laguna_blade || ability.Id == AbilityId.lion_finger_of_death)
            {
                time += ability.ActivationDelay;
            }
            //else if (ability.Id == AbilityId.grimstroke_soul_chain)
            //{
            //    time -= 0.1f;
            //}

            return time /* + 0.1f*/;
        }
    }
}