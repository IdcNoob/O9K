namespace O9K.Evader.Abilities.Heroes.Batrider.Flamebreak
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.batrider_flamebreak)]
    internal class FlamebreakBase : EvaderBaseAbility, IEvadable
    {
        public FlamebreakBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FlamebreakEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}