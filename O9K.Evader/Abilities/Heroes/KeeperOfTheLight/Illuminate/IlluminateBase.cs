namespace O9K.Evader.Abilities.Heroes.KeeperOfTheLight.Illuminate
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.keeper_of_the_light_illuminate)]
    internal class IlluminateBase : EvaderBaseAbility, /* IEvadable, */ IUsable<CounterEnemyAbility>
    {
        public IlluminateBase(Ability9 ability)
            : base(ability)
        {
            //todo fix evadable
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new IlluminateEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new IlluminateUsableCounter(this.Ability, this.ActionManager, this.Menu);
        }
    }
}