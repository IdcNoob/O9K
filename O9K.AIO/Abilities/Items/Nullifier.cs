namespace O9K.AIO.Abilities.Items
{
    using System.Linq;

    using Core.Data;
    using Core.Entities.Abilities.Base;

    using Ensage;

    using TargetManager;

    internal class Nullifier : DebuffAbility
    {
        public Nullifier(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(TargetManager targetManager)
        {
            var target = targetManager.Target;

            if (this.Ability.UnitTargetCast && !target.IsVisible)
            {
                return false;
            }

            if (this.Ability.BreaksLinkens && target.IsBlockingAbilities)
            {
                return false;
            }

            if (target.IsDarkPactProtected)
            {
                return false;
            }

            if (target.HasModifier(ModifierNames.EulsScepter))
            {
                return true;
            }

            if (target.IsInvulnerable)
            {
                if (this.Debuff.UnitTargetCast)
                {
                    return false;
                }

                if (!this.ChainStun(target, true))
                {
                    return false;
                }
            }

            if (target.IsRooted && !this.Ability.UnitTargetCast && target.GetImmobilityDuration() <= 0)
            {
                return false;
            }

            if (target.Abilities.Any(x => x.Id == AbilityId.item_aeon_disk && x.IsReady))
            {
                return false;
            }

            return true;
        }
    }
}