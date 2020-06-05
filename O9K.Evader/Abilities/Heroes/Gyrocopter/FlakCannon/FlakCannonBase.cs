namespace O9K.Evader.Abilities.Heroes.Gyrocopter.FlakCannon
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.gyrocopter_flak_cannon)]
    internal class FlakCannonBase : EvaderBaseAbility, IEvadable
    {
        public FlakCannonBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new FlakCannonEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}