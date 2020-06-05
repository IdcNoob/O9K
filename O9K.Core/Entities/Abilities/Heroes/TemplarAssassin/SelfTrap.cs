namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.templar_assassin_self_trap)]
    public class SelfTrap : AreaOfEffectAbility, IDebuff
    {
        private readonly SpecialData chargeTime;

        public SelfTrap(Ability baseAbility)
            : base(baseAbility)
        {
            this.chargeTime = new SpecialData(baseAbility, "trap_max_charge_duration");
            this.RadiusData = new SpecialData(baseAbility, "trap_radius");
        }

        public override float CastPoint { get; } = 0f;

        public string DebuffModifierName { get; } = "modifier_templar_assassin_trap_slow";

        public bool IsFullyCharged
        {
            get
            {
                return Game.RawGameTime > this.Owner.CreateTime + this.chargeTime.GetValue(this.Level);
            }
        }
    }
}