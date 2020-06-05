namespace O9K.Core.Entities.Abilities.Heroes.Leshrac
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.leshrac_diabolic_edict)]
    public class DiabolicEdict : AreaOfEffectAbility
    {
        public DiabolicEdict(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}