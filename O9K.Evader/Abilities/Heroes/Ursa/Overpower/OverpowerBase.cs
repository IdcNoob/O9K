namespace O9K.Evader.Abilities.Heroes.Ursa.Overpower
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ursa_overpower)]
    internal class OverpowerBase : EvaderBaseAbility, IEvadable
    {
        public OverpowerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new OverpowerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}