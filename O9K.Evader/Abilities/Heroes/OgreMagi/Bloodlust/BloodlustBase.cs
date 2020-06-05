namespace O9K.Evader.Abilities.Heroes.OgreMagi.Bloodlust
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.ogre_magi_bloodlust)]
    internal class BloodlustBase : EvaderBaseAbility, IEvadable
    {
        public BloodlustBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BloodlustEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}