namespace O9K.Evader.Abilities.Heroes.Bane.Enfeeble
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.bane_enfeeble)]
    internal class EnfeebleBase : EvaderBaseAbility, IEvadable
    {
        public EnfeebleBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new EnfeebleEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}