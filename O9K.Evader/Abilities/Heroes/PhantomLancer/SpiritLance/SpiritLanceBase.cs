namespace O9K.Evader.Abilities.Heroes.PhantomLancer.SpiritLance
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phantom_lancer_spirit_lance)]
    internal class SpiritLanceBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public SpiritLanceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpiritLanceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}