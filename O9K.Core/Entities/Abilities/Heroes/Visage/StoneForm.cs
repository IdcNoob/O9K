namespace O9K.Core.Entities.Abilities.Heroes.Visage
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.visage_stone_form_self_cast)]
    public class StoneForm : CircleAbility
    {
        public StoneForm(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "stun_delay");
            this.RadiusData = new SpecialData(baseAbility, "stun_radius");
        }
    }
}