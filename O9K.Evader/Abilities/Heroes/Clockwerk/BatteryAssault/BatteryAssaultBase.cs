namespace O9K.Evader.Abilities.Heroes.Clockwerk.BatteryAssault
{
    using Base;
    using Base.Evadable;
    using Base.Usable.CounterAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.rattletrap_battery_assault)]
    internal class BatteryAssaultBase : EvaderBaseAbility, IEvadable, IUsable<CounterAbility>
    {
        public BatteryAssaultBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new BatteryAssaultEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public CounterAbility GetUsableAbility()
        {
            return new CounterAbility(this.Ability, this.Menu);
        }
    }
}