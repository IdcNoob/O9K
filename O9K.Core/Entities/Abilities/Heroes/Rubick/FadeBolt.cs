namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.rubick_fade_bolt)]
    public class FadeBolt : RangedAbility, INuke
    {
        public FadeBolt(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}