namespace O9K.Evader.Abilities.Heroes.Kunkka.Torrent
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.kunkka_torrent)]
    internal class TorrentBase : EvaderBaseAbility, IEvadable
    {
        public TorrentBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TorrentEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}