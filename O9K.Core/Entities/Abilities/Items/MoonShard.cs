namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_moon_shard)]
    public class MoonShard : PassiveAbility
    {
        public MoonShard(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}