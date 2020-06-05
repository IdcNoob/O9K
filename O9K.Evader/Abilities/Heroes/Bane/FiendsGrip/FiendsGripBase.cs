namespace O9K.Evader.Abilities.Heroes.Bane.FiendsGrip
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bane_fiends_grip)]
    internal class FiendsGripBase : EvaderBaseAbility, IEvadable
    {
        public FiendsGripBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FiendsGripEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}