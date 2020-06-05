namespace O9K.Evader.Abilities.Heroes.TemplarAssassin.PsionicTrap
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.templar_assassin_psionic_trap)]
    internal class PsionicTrapBase : EvaderBaseAbility, IEvadable
    {
        public PsionicTrapBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new PsionicTrapEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}