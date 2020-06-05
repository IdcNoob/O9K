namespace O9K.Evader.Abilities.Heroes.Lion.FingerOfDeath
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lion_finger_of_death)]
    internal class FingerOfDeathBase : EvaderBaseAbility, IEvadable
    {
        public FingerOfDeathBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FingerOfDeathEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}