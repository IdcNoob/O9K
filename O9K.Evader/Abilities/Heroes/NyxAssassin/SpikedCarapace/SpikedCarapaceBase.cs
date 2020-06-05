namespace O9K.Evader.Abilities.Heroes.NyxAssassin.SpikedCarapace
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.nyx_assassin_spiked_carapace)]
    internal class SpikedCarapaceBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public SpikedCarapaceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SpikedCarapaceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}