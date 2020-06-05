namespace O9K.Evader.Abilities.Heroes.TrollWarlord.WhirlingAxesRanged
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.troll_warlord_whirling_axes_ranged)]
    internal class WhirlingAxesRangedBase : EvaderBaseAbility, IEvadable, IUsable<CounterEnemyAbility>
    {
        public WhirlingAxesRangedBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new WhirlingAxesRangedEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterEnemyAbility GetUsableAbility()
        {
            return new CounterEnemyAbility(this.Ability, this.Menu);
        }
    }
}