namespace O9K.Evader.Abilities.Heroes.Lina.LagunaBlade
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lina_laguna_blade)]
    internal class LagunaBladeBase : EvaderBaseAbility, IEvadable
    {
        public LagunaBladeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LagunaBladeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}