namespace O9K.Evader.Abilities.Heroes.Chen.Penitence
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.chen_penitence)]
    internal class PenitenceBase : EvaderBaseAbility, IEvadable
    {
        public PenitenceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PenitenceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}