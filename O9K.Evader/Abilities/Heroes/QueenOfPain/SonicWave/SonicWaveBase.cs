namespace O9K.Evader.Abilities.Heroes.QueenOfPain.SonicWave
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.queenofpain_sonic_wave)]
    internal class SonicWaveBase : EvaderBaseAbility, IEvadable
    {
        public SonicWaveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SonicWaveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}