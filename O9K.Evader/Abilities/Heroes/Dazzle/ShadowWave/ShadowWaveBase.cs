namespace O9K.Evader.Abilities.Heroes.Dazzle.ShadowWave
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dazzle_shadow_wave)]
    internal class ShadowWaveBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public ShadowWaveBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowWaveEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}