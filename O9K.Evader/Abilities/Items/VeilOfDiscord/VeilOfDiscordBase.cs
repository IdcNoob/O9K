namespace O9K.Evader.Abilities.Items.VeilOfDiscord
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.item_veil_of_discord)]
    internal class VeilOfDiscordBase : EvaderBaseAbility, IEvadable
    {
        public VeilOfDiscordBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new VeilOfDiscordEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}