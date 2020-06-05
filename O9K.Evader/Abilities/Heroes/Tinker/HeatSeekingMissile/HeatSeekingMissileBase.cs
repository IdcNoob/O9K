namespace O9K.Evader.Abilities.Heroes.Tinker.HeatSeekingMissile
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.tinker_heat_seeking_missile)]
    internal class HeatSeekingMissileBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public HeatSeekingMissileBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HeatSeekingMissileEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}