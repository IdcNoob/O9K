namespace O9K.Evader.Abilities.Items.LotusOrb
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class LotusOrbUsable : CounterAbility
    {
        public LotusOrbUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool BlockPlayersInput(IObstacle obstacle)
        {
            if (obstacle.IsModifierObstacle)
            {
                return true;
            }

            var abilityId = obstacle.EvadableAbility.Ability.Id;
            return abilityId != AbilityId.lina_laguna_blade && abilityId != AbilityId.lion_finger_of_death;
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (ally.IsSpellShieldProtected)
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            var time = base.GetRequiredTime(ally, enemy, obstacle);
            var ability = obstacle.EvadableAbility.Ability;

            if (ability.Id == AbilityId.lina_laguna_blade || ability.Id == AbilityId.lion_finger_of_death)
            {
                time += ability.ActivationDelay;
            }

            return time /*+ 0.1f*/;
        }
    }
}