namespace O9K.Evader.Abilities.Heroes.Abaddon.CurseOfAvernus
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.abaddon_frostmourne)]
    internal class CurseOfAvernusBase : EvaderBaseAbility, IEvadable
    {
        public CurseOfAvernusBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new CurseOfAvernusEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}