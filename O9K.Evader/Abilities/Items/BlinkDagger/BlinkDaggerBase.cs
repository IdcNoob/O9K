namespace O9K.Evader.Abilities.Items.BlinkDagger
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_blink)]
    internal class BlinkDaggerBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public BlinkDaggerBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkAbility(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}