namespace O9K.Core.Entities.Abilities.Heroes.Terrorblade
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.terrorblade_reflection)]
    public class Reflection : CircleAbility, IDebuff
    {
        public Reflection(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "range");
        }

        public string DebuffModifierName { get; set; } = "modifier_terrorblade_reflection_slow";
    }
}