namespace O9K.Evader.Abilities.Heroes.Morphling.AttributeShiftStrengthGain
{
    using Base;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.morphling_morph_str)]
    internal class AttributeShiftStrengthGainBase : EvaderBaseAbility, IUsable<CounterAbility>
    {
        public AttributeShiftStrengthGainBase(Ability9 ability)
            : base(ability)
        {
        }

        public CounterAbility GetUsableAbility()
        {
            return new AttributeShiftStrengthGainUsable(this.Ability, this.Menu);
        }
    }
}