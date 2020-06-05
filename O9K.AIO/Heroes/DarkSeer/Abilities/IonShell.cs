namespace O9K.AIO.Heroes.DarkSeer.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;
    using Core.Helpers;
    using Core.Managers.Entity;

    using TargetManager;

    internal class IonShell : BuffAbility
    {
        public IonShell(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            return true;
        }

        public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
        {
            var target = targetManager.Target;
            var ally = EntityManager9.Units
                .Where(
                    x => x.IsUnit && x.IsAlly(this.Owner) && x.IsAlive && !x.IsInvulnerable && this.Ability.CanHit(x)
                         && x.Distance(target) < this.Ability.Radius && !x.HasModifier(this.Buff.BuffModifierName))
                .OrderByDescending(x => x.IsMyHero)
                .FirstOrDefault();

            if (ally == null || !this.Ability.UseAbility(ally))
            {
                return false;
            }

            var delay = this.Ability.GetCastDelay(ally);
            comboSleeper.Sleep(delay);
            this.Sleeper.Sleep(delay + 0.5f);
            this.OrbwalkSleeper.Sleep(delay);
            return true;
        }
    }
}