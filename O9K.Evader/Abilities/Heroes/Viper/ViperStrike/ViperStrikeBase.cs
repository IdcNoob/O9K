namespace O9K.Evader.Abilities.Heroes.Viper.ViperStrike
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.viper_viper_strike)]
    internal class ViperStrikeBase : EvaderBaseAbility, IEvadable
    {
        public ViperStrikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ViperStrikeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}