namespace O9K.AIO.Heroes.Grimstroke.Abilities
{
    using System.Linq;

    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using Modes.Combo;

    using TargetManager;

    internal class Soulbind : DebuffAbility
    {
        public Soulbind(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            if (!base.CanHit(targetManager, comboMenu))
            {
                return false;
            }

            var enemies = targetManager.EnemyHeroes.Where(
                x => !x.Equals(targetManager.Target) && x.Distance(targetManager.Target) < this.Ability.Radius);
            if (!enemies.Any())
            {
                return false;
            }

            return true;
        }
    }
}