namespace O9K.Core.Entities.Abilities.Heroes.Necrophos
{
    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.necrolyte_reapers_scythe)]
    public class ReapersScythe : RangedAbility, INuke
    {
        public ReapersScythe(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage_per_health");
            this.ActivationDelayData = new SpecialData(baseAbility, "stun_duration");
        }

        public override bool CanHitSpellImmuneEnemy { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var health = remainingHealth ?? unit.Health;
            var healthDamage = this.DamageData.GetValue(this.Level);

            return new Damage
            {
                [this.DamageType] = (int)((unit.MaximumHealth - health) * healthDamage)
            };
        }
    }
}