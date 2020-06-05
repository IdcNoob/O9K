namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.sniper_assassinate)]
    public class Assassinate : RangedAbility, INuke
    {
        private readonly SpecialData scepterCastPointData;

        public Assassinate(Ability baseAbility)
            : base(baseAbility)
        {
            //todo add stun?
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.scepterCastPointData = new SpecialData(baseAbility, "scepter_cast_point");
        }

        public override float CastPoint
        {
            get
            {
                if (this.Owner.HasAghanimsScepter)
                {
                    return this.scepterCastPointData.GetValue(this.Level);
                }

                return base.CastPoint;
            }
        }
    }
}