namespace O9K.Evader.Abilities.Items.ForceBoots
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_force_boots)]
    internal class ForceBootsBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public ForceBootsBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkLeapAbility(this.Ability, this.Pathfinder, this.ActionManager, this.Menu);
        }
    }
}