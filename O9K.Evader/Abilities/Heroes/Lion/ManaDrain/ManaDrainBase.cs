namespace O9K.Evader.Abilities.Heroes.Lion.ManaDrain
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lion_mana_drain)]
    internal class ManaDrainBase : EvaderBaseAbility, IEvadable
    {
        public ManaDrainBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ManaDrainEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}