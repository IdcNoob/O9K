namespace O9K.Evader.Abilities.Heroes.Rubick.FadeBolt
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.rubick_fade_bolt)]
    internal class FadeBoltBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public FadeBoltBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FadeBoltEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}