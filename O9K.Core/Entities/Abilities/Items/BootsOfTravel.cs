namespace O9K.Core.Entities.Abilities.Items
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.item_travel_boots)]
    [AbilityId(AbilityId.item_travel_boots_2)]
    public class BootsOfTravel : PassiveAbility
    {
        public BootsOfTravel(Ability baseAbility)
            : base(baseAbility)
        {
            this.Name = nameof(AbilityId.item_travel_boots);
            this.Id = AbilityId.item_travel_boots;
        }
    }
    //public class BootsOfTravel : RangedAbility, IChanneled
    //{
    //    public BootsOfTravel(Ability baseAbility)
    //        : base(baseAbility)
    //    {
    //        this.Name = nameof(AbilityId.item_travel_boots);
    //        this.Id = AbilityId.item_travel_boots;
    //        this.ChannelTime = baseAbility.GetChannelTime(0);
    //    }

    //    public override float CastRange { get; } = 9999999;

    //    public float ChannelTime { get; }

    //    public bool IsActivatesOnChannelStart { get; } = false;
    //}
}