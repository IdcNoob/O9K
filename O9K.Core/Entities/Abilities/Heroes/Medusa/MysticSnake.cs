namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.medusa_mystic_snake)]
    public class MysticSnake : RangedAbility, INuke
    {
        public MysticSnake(Ability baseAbility)
            : base(baseAbility)
        {
            //todo stone gaze dmg calcs

            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "snake_damage");
            this.SpeedData = new SpecialData(baseAbility, "initial_speed");
        }
    }
}