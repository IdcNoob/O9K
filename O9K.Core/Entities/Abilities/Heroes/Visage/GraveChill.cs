namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.visage_grave_chill)]
    public class GraveChill : RangedAbility, IDebuff
    {
        public GraveChill(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string DebuffModifierName { get; } = "modifier_visage_grave_chill_debuff";
    }
}