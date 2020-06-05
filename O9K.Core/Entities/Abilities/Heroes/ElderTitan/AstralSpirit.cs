namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.elder_titan_ancestral_spirit)]
    public class AstralSpirit : CircleAbility, INuke
    {
        public AstralSpirit(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "pass_damage");
        }
    }
}