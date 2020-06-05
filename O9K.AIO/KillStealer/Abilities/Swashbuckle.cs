namespace O9K.AIO.KillStealer.Abilities
{
    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.pangolier_swashbuckle)]
    internal class Swashbuckle : KillStealAbility
    {
        public Swashbuckle(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!base.ShouldCast(target))
            {
                return false;
            }

            if (this.Ability.Owner.HasModifier("modifier_pangolier_gyroshell"))
            {
                return false;
            }

            return true;
        }
    }
}