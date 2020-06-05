namespace O9K.Evader.Abilities.Heroes.TemplarAssassin.Refraction
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.templar_assassin_refraction)]
    internal class RefractionBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public RefractionBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}