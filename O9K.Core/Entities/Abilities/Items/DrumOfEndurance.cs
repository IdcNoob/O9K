namespace O9K.Core.Entities.Abilities.Items
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.item_ancient_janggo)]
    public class DrumOfEndurance : AreaOfEffectAbility, IBuff
    {
        public DrumOfEndurance(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string BuffModifierName { get; } = "modifier_item_ancient_janggo_active";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;

        public override bool CanBeCasted(bool checkChanneling = true)
        {
            return this.Charges > 0 && base.CanBeCasted(checkChanneling);
        }
    }
}