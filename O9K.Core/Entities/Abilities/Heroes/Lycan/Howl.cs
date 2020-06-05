namespace O9K.Core.Entities.Abilities.Heroes.Lycan
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.lycan_howl)]
    public class Howl : AreaOfEffectAbility, IDebuff
    {
        public Howl(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_lycan_howl";
    }
}