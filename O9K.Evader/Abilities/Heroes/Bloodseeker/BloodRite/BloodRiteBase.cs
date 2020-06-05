namespace O9K.Evader.Abilities.Heroes.Bloodseeker.BloodRite
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bloodseeker_blood_bath)]
    internal class BloodRiteBase : EvaderBaseAbility, IEvadable
    {
        public BloodRiteBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BloodRiteEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}