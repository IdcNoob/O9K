namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_dagon)]
    [AbilityId(AbilityId.item_dagon_2)]
    [AbilityId(AbilityId.item_dagon_3)]
    [AbilityId(AbilityId.item_dagon_4)]
    [AbilityId(AbilityId.item_dagon_5)]
    public class Dagon : RangedAbility, INuke
    {
        public Dagon(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.Name = nameof(AbilityId.item_dagon_5);
            this.Id = AbilityId.item_dagon_5;
        }

        public override DamageType DamageType { get; } = DamageType.Magical;
    }
}