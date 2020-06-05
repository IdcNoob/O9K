namespace O9K.Evader.Abilities.Base.Usable.CounterAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;

    using Ensage;

    using Evadable;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class CounterInvisibilityAbility : CounterAbility
    {
        public CounterInvisibilityAbility(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
        }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!base.CanBeCasted(ally, enemy, obstacle))
            {
                return false;
            }

            if (obstacle.EvadableAbility.Ability.Id == AbilityId.axe_berserkers_call)
            {
                return true;
            }

            if (ally.HasModifier("modifier_truesight", "item_dust"))
            {
                return false;
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (obstacle.EvadableAbility is TargetableProjectileEvadable projectile && projectile.IsDisjointable)
            {
                return this.ActiveAbility.GetHitTime(ally);
            }

            if (obstacle.EvadableAbility.Ability.Id == AbilityId.zuus_thundergods_wrath)
            {
                return this.ActiveAbility.GetHitTime(ally) + 0.05f;
            }

            return this.ActiveAbility.GetHitTime(ally) - this.ActiveAbility.ActivationDelay;
        }
    }
}