namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.brewmaster_cinder_brew)]
    public class CinderBrew : CircleAbility, IDebuff
    {
        public CinderBrew(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public override float ActivationDelay { get; } = 0.15f; //todo check ?

        public string DebuffModifierName { get; } = "modifier_brewmaster_cinder_brew";
    }
}