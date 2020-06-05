namespace O9K.Evader.Abilities.Heroes.DragonKnight.BreatheFire
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.dragon_knight_breathe_fire)]
    internal class BreatheFireBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public BreatheFireBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BreatheFireEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}