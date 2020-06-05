namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_nullifier)]
    public class Nullifier : RangedAbility, IDebuff
    {
        public Nullifier(Ability baseAbility)
            : base(baseAbility)
        {
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
        }

        public string DebuffModifierName { get; } = "modifier_item_nullifier_mute";
    }
}