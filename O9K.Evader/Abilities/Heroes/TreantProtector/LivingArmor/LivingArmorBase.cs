namespace O9K.Evader.Abilities.Heroes.TreantProtector.LivingArmor
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.treant_living_armor)]
    internal class LivingArmorBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public LivingArmorBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LivingArmorEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}