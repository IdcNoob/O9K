namespace O9K.Evader.Abilities.Heroes.EarthSpirit.GeomagneticGrip
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.earth_spirit_geomagnetic_grip)]
    internal class GeomagneticGripBase : EvaderBaseAbility //, IEvadable
    {
        public GeomagneticGripBase(Ability9 ability)
            : base(ability)
        {
            //todo add evadable/usable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GeomagneticGripEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}