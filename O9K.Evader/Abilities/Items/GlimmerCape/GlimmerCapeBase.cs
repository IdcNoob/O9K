namespace O9K.Evader.Abilities.Items.GlimmerCape
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_glimmer_cape)]
    internal class GlimmerCapeBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public GlimmerCapeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new GlimmerCapeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}