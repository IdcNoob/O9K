namespace O9K.Evader.Abilities.Heroes.Brewmaster.Cyclone
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.brewmaster_storm_cyclone)]
    internal class CycloneBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public CycloneBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CycloneEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}