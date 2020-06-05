namespace O9K.Evader.Abilities.Heroes.Lich.FrostBlast
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lich_frost_nova)]
    internal class FrostBlastBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public FrostBlastBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FrostBlastEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}