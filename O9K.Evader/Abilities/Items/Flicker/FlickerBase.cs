namespace O9K.Evader.Abilities.Items.Flicker
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_flicker)]
    internal class FlickerBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public FlickerBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}