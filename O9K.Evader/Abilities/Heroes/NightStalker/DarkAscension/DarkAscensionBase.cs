namespace O9K.Evader.Abilities.Heroes.NightStalker.DarkAscension
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.night_stalker_darkness)]
    internal class DarkAscensionBase : EvaderBaseAbility, IEvadable
    {
        public DarkAscensionBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new DarkAscensionEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}