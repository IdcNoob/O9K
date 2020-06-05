namespace O9K.Evader.Abilities.Heroes.AncientApparition.ChillingTouch
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ancient_apparition_chilling_touch)]
    internal class ChillingTouchBase : EvaderBaseAbility, IUsable<CounterEnemyAbility>
    {
        public ChillingTouchBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}