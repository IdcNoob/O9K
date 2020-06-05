namespace O9K.Evader.Abilities.Heroes.TrollWarlord.BattleTrance
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.troll_warlord_battle_trance)]
    internal class BattleTranceBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BattleTranceBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BattleTranceEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            //todo improve usage vs axe ult
            return new CounterHealAbility(this.Ability, this.Menu);
        }
    }
}