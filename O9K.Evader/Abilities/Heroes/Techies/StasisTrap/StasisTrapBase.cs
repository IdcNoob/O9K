namespace O9K.Evader.Abilities.Heroes.Techies.StasisTrap
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.techies_stasis_trap)]
    internal class StasisTrapBase : EvaderBaseAbility, IEvadable
    {
        public StasisTrapBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StasisTrapEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}