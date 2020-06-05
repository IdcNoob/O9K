namespace O9K.Evader.Abilities.Heroes.Spectre.SpectralDagger
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spectre_spectral_dagger)]
    internal class SpectralDaggerBase : EvaderBaseAbility /*, IEvadable */, IUsable<CounterEnemyAbility>
    {
        public SpectralDaggerBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpectralDaggerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}