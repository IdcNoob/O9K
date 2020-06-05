namespace O9K.Evader.Abilities.Units.AncientProwlerShaman.Petrify
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spawnlord_master_freeze)]
    internal class PetrifyBase : EvaderBaseAbility, IEvadable
    {
        public PetrifyBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PetrifyEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}