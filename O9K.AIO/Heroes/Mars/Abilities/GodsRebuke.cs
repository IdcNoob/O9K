namespace O9K.AIO.Heroes.Mars.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class GodsRebuke : NukeAbility
    {
        public GodsRebuke(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (targetManager.Target.HasModifier("modifier_mars_spear_impale"))
            {
                return false;
            }

            return true;
        }
    }
}