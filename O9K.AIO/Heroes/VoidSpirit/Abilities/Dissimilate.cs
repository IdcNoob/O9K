namespace O9K.AIO.Heroes.VoidSpirit.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class Dissimilate : AoeAbility
    {
        public Dissimilate(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            if (this.Owner.Distance(targetManager.Target) > 600 && targetManager.Target.GetImmobilityDuration() < 1)
            {
                return false;
            }

            return true;
        }
    }
}