namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.juggernaut_omni_slash)]
    public class Omnislash : RangedAbility
    {
        public Omnislash(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "omni_slash_radius");
            this.DamageData = new SpecialData(baseAbility, "bonus_damage");
        }
    }
}