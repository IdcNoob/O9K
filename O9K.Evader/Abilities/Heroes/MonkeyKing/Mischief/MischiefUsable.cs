namespace O9K.Evader.Abilities.Heroes.MonkeyKing.Mischief
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;
    using Ensage.SDK.Helpers;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class MischiefUsable : CounterAbility
    {
        public MischiefUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (obstacle.EvadableAbility.Ability.Id == AbilityId.rattletrap_hookshot)
            {
                if (obstacle.Caster.Distance(ally) > 700)
                {
                    return false;
                }
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var bonusTime = -0.05f;

            switch (obstacle.EvadableAbility.Ability.Id)
            {
                case AbilityId.beastmaster_wild_axes:
                    bonusTime = -0.15f;
                    break;
            }

            return base.GetRequiredTime(ally, enemy, obstacle) + bonusTime;
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.Use(ally, enemy, obstacle))
            {
                return false;
            }

            UpdateManager.BeginInvoke(
                () =>
                    {
                        if (this.ActiveAbility.CanBeCasted())
                        {
                            this.ActiveAbility.UseAbility();
                        }
                    },
                300);

            return true;
        }
    }
}