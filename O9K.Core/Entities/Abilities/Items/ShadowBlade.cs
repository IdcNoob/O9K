namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_invis_sword)]
    public class ShadowBlade : ActiveAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public ShadowBlade(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "windwalk_fade_time");
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "windwalk_movement_speed");
        }

        public string BuffModifierName { get; } = "modifier_item_invisibility_edge_windwalk";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public override bool IsInvisibility { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}