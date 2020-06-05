namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class PrimalSplit : UntargetableAbility
    {
        public PrimalSplit(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            return this.Owner.Distance(targetManager.Target) < 600;
        }
    }
}