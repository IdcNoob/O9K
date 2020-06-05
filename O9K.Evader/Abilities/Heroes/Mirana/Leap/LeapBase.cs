namespace O9K.Evader.Abilities.Heroes.Mirana.Leap
{
    using Base;
    using Base.Usable.BlinkAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.mirana_leap)]
    internal class LeapBase : EvaderBaseAbility, IUsable<BlinkAbility>
    {
        public LeapBase(Ability9 ability)
            : base(ability)
        {
            //todo evadable ?
        }

        public BlinkAbility GetUsableAbility()
        {
            return new BlinkLeapAbility(this.Ability, this.Pathfinder, this.ActionManager, this.Menu);
        }
    }
}