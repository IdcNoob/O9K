namespace O9K.AIO.Heroes.Grimstroke.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class GrimstrokeDagon : NukeAbility
    {
        public GrimstrokeDagon(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (!targetManager.Target.HasModifier("modifier_grimstroke_soul_chain"))
            {
                return false;
            }

            return true;
        }
    }
}