namespace O9K.Core.Entities.Abilities.Heroes.Techies.RemoteMine
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.techies_remote_mines_self_detonate)]
    public class PinpointDetonate : ActiveAbility
    {
        public PinpointDetonate(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }
    }
}