namespace O9K.Evader.Abilities.Heroes.DeathProphet.SpiritSiphon
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.death_prophet_spirit_siphon)]
    internal class SpiritSiphonBase : EvaderBaseAbility, IEvadable
    {
        public SpiritSiphonBase(Ability9 ability)
            : base(ability)
        {
            //todo add usable ?
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpiritSiphonEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}