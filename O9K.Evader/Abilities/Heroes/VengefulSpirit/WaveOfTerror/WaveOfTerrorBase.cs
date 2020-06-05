namespace O9K.Evader.Abilities.Heroes.VengefulSpirit.WaveOfTerror
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.vengefulspirit_wave_of_terror)]
    internal class WaveOfTerrorBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public WaveOfTerrorBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WaveOfTerrorEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}