namespace O9K.Core.Entities.Abilities.Heroes.Viper
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.viper_nethertoxin)]
    public class Nethertoxin : CircleAbility, IDisable
    {
        public Nethertoxin(Ability baseAbility)
            : base(baseAbility)
        {
            //todo fix applies state ?

            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
        }

        public UnitState AppliesUnitState
        {
            get
            {
                var talent = this.Owner.GetAbilityById(AbilityId.special_bonus_unique_viper_3);
                if (talent?.Level > 0)
                {
                    return UnitState.PassivesDisabled | UnitState.Silenced;
                }

                return UnitState.PassivesDisabled;
            }
        }
    }
}