namespace O9K.Evader.Abilities.Heroes.Clinkz.BurningArmy
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.clinkz_burning_army)]
    internal class BurningArmyBase : EvaderBaseAbility, IEvadable
    {
        public BurningArmyBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BurningArmyEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}