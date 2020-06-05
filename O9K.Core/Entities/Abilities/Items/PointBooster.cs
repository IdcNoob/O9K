namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_point_booster)]
    public class PointBooster : PassiveAbility
    {
        public PointBooster(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}