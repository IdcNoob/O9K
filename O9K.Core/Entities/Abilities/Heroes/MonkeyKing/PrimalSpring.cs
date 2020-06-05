namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
    using Base;
    using Base.Components;

    using Ensage;

    using Entities.Units;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.monkey_king_primal_spring)]
    public class PrimalSpring : CircleAbility, IChanneled
    {
        private readonly SpecialData castRangeData;

        public PrimalSpring(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
            this.RadiusData = new SpecialData(baseAbility, "impact_radius");
            this.DamageData = new SpecialData(baseAbility, "impact_damage");
            this.castRangeData = new SpecialData(baseAbility, "max_distance");
        }

        public override float ActivationDelay
        {
            get
            {
                return this.ChannelTime;
            }
        }

        public float ChannelTime { get; }

        public bool IsActivatesOnChannelStart { get; } = true;

        public override float Speed { get; } = 1300;

        protected override float BaseCastRange
        {
            get
            {
                return this.castRangeData.GetValue(this.Level);
            }
        }

        public int GetCurrentDamage(Unit9 unit)
        {
            return (int)(this.GetDamage(unit) * (this.BaseAbility.ChannelTime / this.ChannelTime));
        }
    }
}