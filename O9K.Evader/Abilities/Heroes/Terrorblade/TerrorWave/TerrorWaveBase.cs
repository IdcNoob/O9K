namespace O9K.Evader.Abilities.Heroes.Terrorblade.TerrorWave
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;

    //  [AbilityId((AbilityId)425)]
    internal class TerrorWaveBase : EvaderBaseAbility, IEvadable
    {
        public TerrorWaveBase(Ability9 ability)
            : base(ability)
        {
            //todo add
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TerrorWaveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}