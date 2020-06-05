namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.winter_wyvern_splinter_blast)]
    public class SplinterBlast : RangedAreaOfEffectAbility
    {
        public SplinterBlast(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "split_radius");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
        }
    }
}