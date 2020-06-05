namespace O9K.Evader.Abilities.Heroes.Oracle.FatesEdict
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.oracle_fates_edict)]
    internal class FatesEdictBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>, IUsable<DisableAbility>
    {
        public FatesEdictBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FatesEdictEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        CounterAbility IUsable<CounterAbility>.GetUsableAbility()
        {
            //todo fix cast on fates edict debuff
            return new CounterAbility(this.Ability, this.Menu);
        }

        DisableAbility IUsable<DisableAbility>.GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}