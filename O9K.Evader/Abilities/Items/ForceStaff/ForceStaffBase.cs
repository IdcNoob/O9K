namespace O9K.Evader.Abilities.Items.ForceStaff
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_force_staff)]
    internal class ForceStaffBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public ForceStaffBase(Ability9 ability)
            : base(ability)
        {
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkLeapAbility(this.Ability, this.Pathfinder, this.ActionManager, this.Menu);
        }
    }
}