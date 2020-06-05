namespace O9K.Evader.Abilities.Heroes.Underlord.Firestorm
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.abyssal_underlord_firestorm)]
    internal class FirestormBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public FirestormBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FirestormEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}