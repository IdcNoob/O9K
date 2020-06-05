namespace O9K.Evader.Abilities.Heroes.QueenOfPain.ShadowStrike
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.queenofpain_shadow_strike)]
    internal class ShadowStrikeBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public ShadowStrikeBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShadowStrikeEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}