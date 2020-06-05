namespace O9K.Evader.Abilities.Heroes.SandKing.SandStorm
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.sandking_sand_storm)]
    internal class SandStormBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public SandStormBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterInvisibilityAbility(this.Ability, this.Menu);
        }
    }
}