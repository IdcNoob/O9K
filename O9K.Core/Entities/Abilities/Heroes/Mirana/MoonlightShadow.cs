namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.mirana_invis)]
    public class MoonlightShadow : AreaOfEffectAbility
    {
        public MoonlightShadow(Ability ability)
            : base(ability)
        {
        }

        public override bool IsInvisibility { get; } = true;

        public override float Radius { get; } = 9999999;
    }
}