namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
    using Core.Entities.Abilities.Base.Types;
    using Core.Entities.Abilities.Heroes.Visage;
    using Core.Entities.Metadata;
    using Core.Entities.Units;

    using Ensage;

    [AbilityId(AbilityId.visage_soul_assumption)]
    internal class SoulAssumptionNukeAbility : OldNukeAbility
    {
        private readonly SoulAssumption soulAssumption;

        public SoulAssumptionNukeAbility(INuke ability)
            : base(ability)
        {
            this.soulAssumption = (SoulAssumption)ability;
        }

        public override bool ShouldCast(Unit9 target)
        {
            if (!base.ShouldCast(target))
            {
                return false;
            }

            if (!this.soulAssumption.MaxCharges && this.soulAssumption.GetDamage(target) < target.Health)
            {
                return false;
            }

            return true;
        }
    }
}