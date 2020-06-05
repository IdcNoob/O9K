namespace O9K.Evader.Abilities.Heroes.LoneDruid.SavageRoar
{
    using Base;
    using Base.Evadable;
    using Base.Usable.DisableAbility;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.lone_druid_savage_roar)]
    [AbilityId(AbilityId.lone_druid_savage_roar_bear)]
    internal class SavageRoarBase : EvaderBaseAbility, IEvadable, IUsable<DisableAbility>
    {
        public SavageRoarBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new SavageRoarEvadable(this.Ability, this.Pathfinder, this.Menu);
        }

        public DisableAbility GetUsableAbility()
        {
            return new DisableAbility(this.Ability, this.Menu);
        }
    }
}