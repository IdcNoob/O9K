namespace O9K.AIO.KillStealer.Abilities
{
    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.necrolyte_reapers_scythe)]
    internal class ReapersScythe : KillStealAbility
    {
        public ReapersScythe(ActiveAbility ability)
            : base(ability)
        {
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!base.ShouldCast(target))
            {
                return false;
            }

            if (target.CanReincarnate)
            {
                return false;
            }

            return true;
        }
    }
}