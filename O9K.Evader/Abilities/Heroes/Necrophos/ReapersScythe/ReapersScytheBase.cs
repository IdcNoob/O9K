namespace O9K.Evader.Abilities.Heroes.Necrophos.ReapersScythe
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.necrolyte_reapers_scythe)]
    internal class ReapersScytheBase : EvaderBaseAbility, IUsable<DisableAbility>, IEvadable
    {
        public ReapersScytheBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ReapersScytheEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}