namespace O9K.Evader.Abilities.Heroes.Pangolier.ShieldCrash
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.pangolier_shield_crash)]
    internal class ShieldCrashBase : EvaderBaseAbility, IEvadable
    {
        public ShieldCrashBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new ShieldCrashEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}