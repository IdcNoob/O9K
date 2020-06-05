namespace O9K.Evader.Abilities.Heroes.Timbersaw.WhirlingDeath
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.shredder_whirling_death)]
    internal class WhirlingDeathBase : EvaderBaseAbility, IEvadable
    {
        public WhirlingDeathBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WhirlingDeathEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}