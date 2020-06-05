namespace O9K.Evader.Abilities.Heroes.Mars.SpearOfMars
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mars_spear)]
    internal class SpearOfMarsBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>, IUsable<CounterEnemyAbility>
    {
        public SpearOfMarsBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpearOfMarsEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }

        CounterEnemyAbility IUsable<CounterEnemyAbility>.GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}