namespace O9K.Evader.Abilities.Heroes.Tiny.TreeGrab
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tiny_craggy_exterior)]
    internal class TreeGrabBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public TreeGrabBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new TreeGrabUsable(this.Ability, this.Menu);
        }
    }
}