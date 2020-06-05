namespace O9K.Evader.Abilities.Heroes.Venomancer.PoisonNova
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.venomancer_poison_nova)]
    internal class PoisonNovaBase : EvaderBaseAbility, IEvadable
    {
        public PoisonNovaBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PoisonNovaEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}