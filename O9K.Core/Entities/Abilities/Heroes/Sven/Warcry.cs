namespace O9K.Core.Entities.Abilities.Heroes.Sven
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.sven_warcry)]
    public class Warcry : AreaOfEffectAbility, ISpeedBuff
    {
        private readonly SpecialData bonusMoveSpeedData;

        public Warcry(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.bonusMoveSpeedData = new SpecialData(baseAbility, "movespeed");
        }

        public string BuffModifierName { get; } = "modifier_sven_warcry";

        public bool BuffsAlly { get; } = true;

        public bool BuffsOwner { get; } = true;

        public float GetSpeedBuff(Unit9 unit)
        {
            return (unit.Speed * this.bonusMoveSpeedData.GetValue(this.Level)) / 100;
        }
    }
}