namespace O9K.Evader.Abilities.Heroes.Luna.LucentBeam
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.luna_lucent_beam)]
    internal class LucentBeamBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>, IUsable<CounterEnemyAbility>
    {
        public LucentBeamBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LucentBeamEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }

        CounterEnemyAbility IUsable<CounterEnemyAbility>.GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}