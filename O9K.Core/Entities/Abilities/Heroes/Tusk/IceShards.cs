namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.tusk_ice_shards)]
    public class IceShards : LineAbility, INuke
    {
        public IceShards(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "shard_width");
            this.SpeedData = new SpecialData(baseAbility, "shard_speed");
            this.DamageData = new SpecialData(baseAbility, "shard_damage");
        }
    }
}