namespace O9K.Evader.Abilities.Heroes.Kunkka.Tidebringer
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.kunkka_tidebringer)]
    internal class TidebringerBase : EvaderBaseAbility, IEvadable
    {
        public TidebringerBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new TidebringerEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}