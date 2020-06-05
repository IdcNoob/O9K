namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.silencer_curse_of_the_silent)]
    public class ArcaneCurse : CircleAbility, IDebuff
    {
        public ArcaneCurse(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_silencer_curse_of_the_silent";
    }
}