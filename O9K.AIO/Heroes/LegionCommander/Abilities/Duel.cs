namespace O9K.AIO.Heroes.LegionCommander.Abilities
{
    using AIO.Abilities;

    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class Duel : TargetableAbility
    {
        public Duel(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (!target.IsVisible || target.IsInvulnerable || target.IsLinkensProtected)
            {
                return false;
            }

            return true;
        }
    }
}