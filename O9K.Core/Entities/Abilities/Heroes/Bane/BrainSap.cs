namespace O9K.Core.Entities.Abilities.Heroes.Bane
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.bane_brain_sap)]
    public class BrainSap : RangedAbility, INuke
    {
        public BrainSap(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "brain_sap_damage");
        }

        public override float CastPoint
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return base.CastPoint / 2f;
                }

                return base.CastPoint;
            }
        }
    }
}