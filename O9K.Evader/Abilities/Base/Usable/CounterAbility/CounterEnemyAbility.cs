namespace O9K.Evader.Abilities.Base.Usable.CounterAbility
{
    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Units;
    using Core.Prediction.Data;

    using Ensage;

    using Metadata;

    using Pathfinder.Obstacles;

    internal class CounterEnemyAbility : CounterAbility
    {
        public CounterEnemyAbility(Ability9 ability, IMainMenu menu)
            : base(ability, menu)
        {
            //todo merge with disable

            if (ability is IDisable disable)
            {
                this.AppliesUnitState = disable.AppliesUnitState;
            }
        }

        public UnitState AppliesUnitState { get; }

        public override bool CanBeCasted(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            if (!this.IsAbilityEnabled(obstacle))
            {
                return false;
            }

            if (this.ActiveAbility.UnitTargetCast && (!enemy.IsVisible || enemy.IsInvulnerable || enemy.IsUntargetable))
            {
                return false;
            }

            if (this.ActiveAbility.BreaksLinkens && enemy.IsBlockingAbilities)
            {
                return false;
            }

            if (!this.Owner.Equals(ally))
            {
                return false;
            }

            if (!this.Ability.CanBeCasted(false))
            {
                return false;
            }

            if (!this.ActiveAbility.CanHit(enemy))
            {
                return false;
            }

            var id = obstacle.EvadableAbility.Ability.Id;
            if (id == AbilityId.item_flask || id == AbilityId.item_clarity)
            {
                if (!enemy.CanDie)
                {
                    return false;
                }
            }

            return true;
        }

        public override float GetRequiredTime(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            return this.ActiveAbility.GetCastDelay(enemy.Position);
        }

        public override bool Use(Unit9 ally, Unit9 enemy, IObstacle obstacle)
        {
            this.MoveCamera(enemy.Position);
            var use = this.ActiveAbility.UseAbility(enemy, HitChance.Medium, false, true);
            if (use)
            {
                enemy.SetExpectedUnitState(this.AppliesUnitState, this.ActiveAbility.GetHitTime(enemy) + 0.3f);
            }

            return use;
        }
    }
}