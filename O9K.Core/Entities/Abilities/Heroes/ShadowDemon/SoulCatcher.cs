namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.shadow_demon_soul_catcher)]
    public class SoulCatcher : CircleAbility, IDebuff
    {
        public SoulCatcher(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_shadow_demon_soul_catcher";
    }
}