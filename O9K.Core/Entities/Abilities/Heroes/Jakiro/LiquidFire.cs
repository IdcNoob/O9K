namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.jakiro_liquid_fire)]
    public class LiquidFire : OrbAbility, IHarass
    {
        public LiquidFire(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}