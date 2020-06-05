namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.doom_bringer_scorched_earth)]
    public class ScorchedEarth : AreaOfEffectAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public ScorchedEarth(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "bonus_movement_speed_pct");
        }

        public string BuffModifierName { get; } = "modifier_doom_bringer_scorched_earth_effect";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}