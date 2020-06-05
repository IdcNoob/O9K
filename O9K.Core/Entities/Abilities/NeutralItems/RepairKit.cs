namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_repair_kit)]
    public class RepairKit : RangedAbility
    {
        public RepairKit(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}