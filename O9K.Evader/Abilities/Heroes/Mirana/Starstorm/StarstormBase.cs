namespace O9K.Evader.Abilities.Heroes.Mirana.Starstorm
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mirana_starfall)]
    internal class StarstormBase : EvaderBaseAbility, IEvadable
    {
        public StarstormBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StarstormEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}