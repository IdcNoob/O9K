namespace O9K.Evader.Abilities.Heroes.OgreMagi.Ignite
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ogre_magi_ignite)]
    internal class IgniteBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public IgniteBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IgniteEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}