namespace O9K.Evader.Abilities.Units.SatyrTormenter.Shockwave
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.satyr_hellcaller_shockwave)]
    internal class ShockwaveBase : EvaderBaseAbility, IEvadable
    {
        public ShockwaveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShockwaveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}