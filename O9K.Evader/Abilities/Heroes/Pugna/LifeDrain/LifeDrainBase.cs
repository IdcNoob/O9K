namespace O9K.Evader.Abilities.Heroes.Pugna.LifeDrain
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pugna_life_drain)]
    internal class LifeDrainBase : EvaderBaseAbility, IEvadable
    {
        public LifeDrainBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LifeDrainEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}