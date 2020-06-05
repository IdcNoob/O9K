namespace O9K.Evader.Abilities.Heroes.Abaddon.AphoticShield
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.abaddon_aphotic_shield)]
    internal class AphoticShieldBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public AphoticShieldBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new AphoticShieldEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new AphoticShieldUsable(this.Ability, this.ActionManager, this.Menu);
        }
    }
}