namespace O9K.AIO.Abilities.Items
{
    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class EulsScepterOfDivinity : DisableAbility
    {
        public EulsScepterOfDivinity(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!target.IsInNormalState && !target.IsTeleporting && !target.IsChanneling)
            {
                return false;
            }

            if (target.HasModifier("modifier_lina_laguna_blade", "modifier_lion_finger_of_death"))
            {
                return false;
            }

            return this.ShouldForceCast(targetManager);
        }

        public bool ShouldForceCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (this.Disable.UnitTargetCast && target.IsBlockingAbilities)
            {
                return false;
            }

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.IsInvulnerable)
            {
                return false;
            }

            return true;
        }
    }
}