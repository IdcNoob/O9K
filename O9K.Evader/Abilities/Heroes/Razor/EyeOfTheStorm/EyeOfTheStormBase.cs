namespace O9K.Evader.Abilities.Heroes.Razor.EyeOfTheStorm
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.razor_eye_of_the_storm)]
    internal class EyeOfTheStormBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public EyeOfTheStormBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EyeOfTheStormEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}