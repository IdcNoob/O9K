namespace O9K.Evader.Abilities.Heroes.Grimstroke.StrokeOfFate
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.grimstroke_dark_artistry)]
    internal class StrokeOfFateBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public StrokeOfFateBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new StrokeOfFateEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}