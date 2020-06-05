namespace O9K.Evader.Abilities.Heroes.Silencer.ArcaneCurse
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.silencer_curse_of_the_silent)]
    internal class ArcaneCurseBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ArcaneCurseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ArcaneCurseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}