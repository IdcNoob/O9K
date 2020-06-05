namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.grimstroke_soul_chain)]
    public class Soulbind : RangedAreaOfEffectAbility, IDebuff
    {
        public Soulbind(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "chain_latch_radius");
        }

        public string DebuffModifierName { get; } = "modifier_grimstroke_soul_chain";
    }
}