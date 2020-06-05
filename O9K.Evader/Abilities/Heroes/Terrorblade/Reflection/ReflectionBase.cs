namespace O9K.Evader.Abilities.Heroes.Terrorblade.Reflection
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.terrorblade_reflection)]
    internal class ReflectionBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ReflectionBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ReflectionEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}