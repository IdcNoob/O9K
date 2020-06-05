namespace O9K.Evader.Abilities.Heroes.Huskar.BurningSpear
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.huskar_burning_spear)]
    internal class BurningSpearBase : EvaderBaseAbility, IEvadable
    {
        public BurningSpearBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BurningSpearEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}