namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.treant_natures_grasp)]
    public class NaturesGrasp : LineAbility, IHarass
    {
        public NaturesGrasp(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "initial_latch_delay");
            this.DamageData = new SpecialData(baseAbility, "damage_per_second");
            this.RadiusData = new SpecialData(baseAbility, "latch_range");
        }
    }
}