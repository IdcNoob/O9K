namespace O9K.Evader.Abilities.Units.AncientProwlerShaman.Desecrate
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.spawnlord_master_stomp)]
    internal class DesecrateBase : EvaderBaseAbility, IEvadable
    {
        public DesecrateBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DesecrateEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}