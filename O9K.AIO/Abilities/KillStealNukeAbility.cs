namespace O9K.AIO.Abilities
{
    using Core.Entities.Abilities.Base;

    using TargetManager;

    internal class KillStealNukeAbility : NukeAbility
    {
        public KillStealNukeAbility(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            if (!base.ShouldCast(targetManager))
            {
                return false;
            }

            var damage = this.Ability.GetDamage(targetManager.Target);
            if (damage <= targetManager.Target.Health)
            {
                return false;
            }

            return true;
        }
    }
}