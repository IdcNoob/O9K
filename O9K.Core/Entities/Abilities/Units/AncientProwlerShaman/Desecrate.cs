namespace O9K.Core.Entities.Abilities.Units.AncientProwlerShaman
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.spawnlord_master_stomp)]
    public class Desecrate : AreaOfEffectAbility, INuke
    {
        public Desecrate(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "damage");
        }
    }
}