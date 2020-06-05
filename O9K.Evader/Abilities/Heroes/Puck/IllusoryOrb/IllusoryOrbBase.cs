namespace O9K.Evader.Abilities.Heroes.Puck.IllusoryOrb
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.puck_illusory_orb)]
    internal class IllusoryOrbBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public IllusoryOrbBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IllusoryOrbEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}