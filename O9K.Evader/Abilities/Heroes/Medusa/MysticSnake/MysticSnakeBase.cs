namespace O9K.Evader.Abilities.Heroes.Medusa.MysticSnake
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.medusa_mystic_snake)]
    internal class MysticSnakeBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public MysticSnakeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new MysticSnakeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}