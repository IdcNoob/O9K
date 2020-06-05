namespace O9K.Evader.Abilities.Heroes.EarthSpirit.Magnetize
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.earth_spirit_magnetize)]
    internal class MagnetizeBase : EvaderBaseAbility, IEvadable
    {
        public MagnetizeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MagnetizeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}