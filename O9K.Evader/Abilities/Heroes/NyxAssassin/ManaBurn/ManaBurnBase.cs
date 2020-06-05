namespace O9K.Evader.Abilities.Heroes.NyxAssassin.ManaBurn
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.nyx_assassin_mana_burn)]
    internal class ManaBurnBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ManaBurnBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ManaBurnEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}