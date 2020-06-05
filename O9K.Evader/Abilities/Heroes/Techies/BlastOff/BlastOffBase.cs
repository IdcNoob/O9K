namespace O9K.Evader.Abilities.Heroes.Techies.BlastOff
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.techies_suicide)]
    internal class BlastOffBase : EvaderBaseAbility, IEvadable
    {
        public BlastOffBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BlastOffEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}