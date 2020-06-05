namespace O9K.Evader.Abilities.Heroes.Disruptor.Glimpse
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.disruptor_glimpse)]
    internal class GlimpseBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public GlimpseBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GlimpseEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}