namespace O9K.Evader.Abilities.Heroes.StormSpirit.ElectricVortex
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.storm_spirit_electric_vortex)]
    internal class ElectricVortexBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public ElectricVortexBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ElectricVortexEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}