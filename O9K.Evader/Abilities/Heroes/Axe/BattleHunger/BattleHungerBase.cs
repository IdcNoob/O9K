namespace O9K.Evader.Abilities.Heroes.Axe.BattleHunger
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.axe_battle_hunger)]
    internal class BattleHungerBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public BattleHungerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BattleHungerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}