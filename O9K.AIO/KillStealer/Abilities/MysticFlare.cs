namespace O9K.AIO.KillStealer.Abilities
{
    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.skywrath_mage_mystic_flare)]
    internal class MysticFlare : KillStealAbility
    {
        public MysticFlare(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!base.ShouldCast(target))
            {
                return false;
            }

            if (target.GetImmobilityDuration() < 1f)
            {
                return false;
            }

            return true;
        }
    }
}