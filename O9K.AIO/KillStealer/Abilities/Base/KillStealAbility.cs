namespace O9K.AIO.KillStealer.Abilities.Base
{
    using System.Linq;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Units;
    using Core.Managers.Entity;
    using Core.Prediction.Data;

    using Ensage;

    internal class KillStealAbility
    {
        public KillStealAbility(ActiveAbility ability)
        {
            this.Ability = ability;
        }

        public ActiveAbility Ability { get; }

        public virtual bool CanHit(Unit9 target)
        {
            return this.Ability.CanHit(target);
        }

        public virtual bool ShouldCast(Unit9 target)
        {
            var borrowedTime = target.Abilities.FirstOrDefault(
                x => x.Id == AbilityId.abaddon_borrowed_time && (x.IsReady || x.TimeSinceCasted < 0.5f));
            if (borrowedTime != null)
            {
                return false;
            }

            return true;
        }

        public virtual bool UseAbility(Unit9 target)
        {
            return this.Ability.UseAbility(target, EntityManager9.EnemyHeroes, HitChance.Medium);
        }
    }
}