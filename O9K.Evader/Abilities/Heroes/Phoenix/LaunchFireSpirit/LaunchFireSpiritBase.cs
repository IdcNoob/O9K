namespace O9K.Evader.Abilities.Heroes.Phoenix.LaunchFireSpirit
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.phoenix_launch_fire_spirit)]
    internal class LaunchFireSpiritBase : EvaderBaseAbility, IEvadable
    {
        public LaunchFireSpiritBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new LaunchFireSpiritEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}