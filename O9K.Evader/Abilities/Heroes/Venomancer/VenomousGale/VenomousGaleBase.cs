namespace O9K.Evader.Abilities.Heroes.Venomancer.VenomousGale
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.venomancer_venomous_gale)]
    internal class VenomousGaleBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public VenomousGaleBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new VenomousGaleEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}