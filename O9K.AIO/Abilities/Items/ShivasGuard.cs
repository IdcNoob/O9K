namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;

    using Modes.Combo;

    using TargetManager;

    internal class ShivasGuard : DebuffAbility
    {
        public ShivasGuard(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;

            if (this.Owner.Distance(target) > 600)
            {
                return false;
            }

            if (target.IsMagicImmune && !this.Ability.PiercesMagicImmunity(target))
            {
                return false;
            }

            return true;
        }
    }
}