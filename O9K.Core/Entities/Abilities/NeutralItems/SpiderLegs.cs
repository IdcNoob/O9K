namespace O9K.Core.Entities.Abilities.NeutralItems
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_spider_legs)]
    public class SpiderLegs : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData speedBuffData;

        public SpiderLegs(Ability baseAbility)
            : base(baseAbility)
        {
            this.speedBuffData = new SpecialData(baseAbility, "bonus_movement_speed_active");
        }

        public string BuffModifierName { get; } = "modifier_item_spider_legs_active";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.speedBuffData.GetValue(this.Level)) / 100;
        }
    }
}