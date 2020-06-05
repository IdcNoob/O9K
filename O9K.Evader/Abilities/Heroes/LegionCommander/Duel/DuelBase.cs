namespace O9K.Evader.Abilities.Heroes.LegionCommander.Duel
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.legion_commander_duel)]
    internal class DuelBase : EvaderBaseAbility, IEvadable
    {
        public DuelBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DuelEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}