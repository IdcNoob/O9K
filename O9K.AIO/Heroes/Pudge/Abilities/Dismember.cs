namespace O9K.AIO.Heroes.Pudge.Abilities
{
    using AIO.Abilities;
    using AIO.Modes.Combo;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class Dismember : DisableAbility
    {
        public Dismember(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
        {
            var target = targetManager.Target;
            if (targetManager.Target.HasModifier("modifier_pudge_meat_hook") && target.Distance(this.Owner) < 500)
            {
                return true;
            }

            return base.CanHit(targetManager, comboMenu);
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;
            if (target.IsVisible && targetManager.Target.HasModifier("modifier_pudge_meat_hook") && target.Distance(this.Owner) < 500)
            {
                return true;
            }

            return base.ShouldCast(targetManager);
        }
    }
}