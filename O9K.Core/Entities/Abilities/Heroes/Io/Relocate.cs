namespace O9K.Core.Entities.Abilities.Heroes.Io
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.wisp_relocate)]
    public class Relocate : RangedAbility
    {
        public Relocate(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "cast_delay");
        }

        public override float CastRange { get; } = 9999999;
    }
}