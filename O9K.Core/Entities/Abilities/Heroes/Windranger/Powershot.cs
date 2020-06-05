namespace O9K.Core.Entities.Abilities.Heroes.Windranger
{
    using Base;
    using Base.Components;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.windrunner_powershot)]
    public class Powershot : LineAbility, IChanneled, INuke
    {
        public Powershot(Ability baseAbility)
            : base(baseAbility)
        {
            this.ChannelTime = baseAbility.GetChannelTime(0);
            this.RadiusData = new SpecialData(baseAbility, "arrow_width");
            this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
            this.DamageData = new SpecialData(baseAbility, "powershot_damage");
        }

        public override float ActivationDelay
        {
            get
            {
                return this.ChannelTime;
            }
        }

        public float ChannelTime { get; }

        public override bool HasAreaOfEffect { get; } = false;

        public bool IsActivatesOnChannelStart { get; } = true;

        public int GetCurrentDamage(Unit9 unit)
        {
            return (int)(this.GetDamage(unit) * (this.BaseAbility.ChannelTime / this.ChannelTime));
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            //todo improve damage reduction

            var damage = base.GetRawDamage(unit, remainingHealth);

            damage[this.DamageType] *= 0.8f;

            return damage;
        }
    }
}