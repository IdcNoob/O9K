namespace O9K.Evader.Abilities.Items.MantaStyle
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class MantaStyleUsable : CounterAbility
    {
        public MantaStyleUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var bonusTime = -0.05f;

            if (obstacle.EvadableAbility.Ability.Id == AbilityId.juggernaut_omni_slash)
            {
            }

            return base.GetRequiredTime(ally, enemy, obstacle) + bonusTime;
        }
    }
}