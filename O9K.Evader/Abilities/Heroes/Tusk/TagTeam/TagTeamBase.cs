namespace O9K.Evader.Abilities.Heroes.Tusk.TagTeam
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tusk_tag_team)]
    internal class TagTeamBase : EvaderBaseAbility, IEvadable
    {
        public TagTeamBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TagTeamEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}