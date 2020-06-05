namespace O9K.Evader.Abilities.Heroes.Bristleback.Bristleback
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bristleback_bristleback)]
    internal class BristlebackBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public BristlebackBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new BristlebackUsable(this.Ability, this.Menu);
        }
    }
}