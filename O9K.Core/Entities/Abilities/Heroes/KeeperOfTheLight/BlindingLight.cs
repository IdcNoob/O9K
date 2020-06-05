namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.keeper_of_the_light_blinding_light)]
    public class BlindingLight : CircleAbility, IDebuff, INuke
    {
        public BlindingLight(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public override DamageType DamageType { get; } = DamageType.Magical; //todo remove?

        public string DebuffModifierName { get; } = "modifier_keeper_of_the_light_blinding_light";
    }
}