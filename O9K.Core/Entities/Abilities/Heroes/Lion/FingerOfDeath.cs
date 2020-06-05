namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.lion_finger_of_death)]
    public class FingerOfDeath : RangedAbility, INuke
    {
        private readonly SpecialData damagePerKillData;

        private readonly SpecialData scepterDamageData;

        public FingerOfDeath(Ability baseAbility)
            : base(baseAbility)
        {
            this.ActivationDelayData = new SpecialData(baseAbility, "damage_delay");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.damagePerKillData = new SpecialData(baseAbility, "damage_per_kill");
            this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var kills = this.Owner.GetModifier("modifier_lion_finger_of_death_kill_counter")?.StackCount ?? 0;
            var damage = new Damage
            {
                [this.DamageType] = kills * this.damagePerKillData.GetValue(this.Level)
            };

            if (this.Owner.HasAghanimsScepter)
            {
                damage[this.DamageType] += this.scepterDamageData.GetValue(this.Level);
            }
            else
            {
                damage[this.DamageType] += this.DamageData.GetValue(this.Level);
            }

            return damage;
        }
    }
}