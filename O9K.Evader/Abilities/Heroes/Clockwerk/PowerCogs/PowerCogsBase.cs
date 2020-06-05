namespace O9K.Evader.Abilities.Heroes.Clockwerk.PowerCogs
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.rattletrap_power_cogs)]
    internal class PowerCogsBase : EvaderBaseAbility, IEvadable
    {
        public PowerCogsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PowerCogsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}