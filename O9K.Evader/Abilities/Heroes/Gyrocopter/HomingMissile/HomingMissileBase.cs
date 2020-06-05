namespace O9K.Evader.Abilities.Heroes.Gyrocopter.HomingMissile
{
    using Base;
    using Base.Evadable;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Metadata;

    using Ensage;

    [AbilityId(AbilityId.gyrocopter_homing_missile)]
    internal class HomingMissileBase : EvaderBaseAbility, IEvadable
    {
        public HomingMissileBase(Ability9 ability)
            : base(ability)
        {
        }

        public EvadableAbility GetEvadableAbility()
        {
            return new HomingMissileEvadable(this.Ability, this.Pathfinder, this.Menu);
        }
    }
}