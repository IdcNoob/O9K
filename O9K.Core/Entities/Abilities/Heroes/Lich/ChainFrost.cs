namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.lich_chain_frost)]
    public class ChainFrost : RangedAbility, INuke
    {
        public ChainFrost(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}