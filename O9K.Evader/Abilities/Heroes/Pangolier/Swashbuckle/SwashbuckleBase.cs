namespace O9K.Evader.Abilities.Heroes.Pangolier.Swashbuckle
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pangolier_swashbuckle)]
    internal class SwashbuckleBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public SwashbuckleBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new SwashbuckleUsable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}