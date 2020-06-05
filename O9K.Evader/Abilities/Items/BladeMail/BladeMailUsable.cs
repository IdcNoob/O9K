namespace O9K.Evader.Abilities.Items.BladeMail
{
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class BladeMailUsable : CounterAbility
    {
        public BladeMailUsable(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            switch (obstacle.EvadableAbility.Ability.DamageType)
            {
                case DamageType.Magical:
                    return !obstacle.EvadableAbility.Owner.IsMagicImmune;
                case DamageType.Physical:
                    return !obstacle.EvadableAbility.Owner.IsAttackImmune;
            }

            return true;
        }
    }
}