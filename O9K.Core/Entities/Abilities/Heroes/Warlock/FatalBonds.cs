namespace O9K.Core.Entities.Abilities.Heroes.Warlock
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.warlock_fatal_bonds)]
    public class FatalBonds : RangedAreaOfEffectAbility, IDebuff
    {
        public FatalBonds(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "search_aoe");
        }

        public string DebuffModifierName { get; } = "modifier_warlock_fatal_bonds";
    }
}