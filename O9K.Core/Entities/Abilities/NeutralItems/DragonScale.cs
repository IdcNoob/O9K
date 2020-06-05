namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_dragon_scale)]
    public class DragonScale : PassiveAbility
    {
        public DragonScale(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}