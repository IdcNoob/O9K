namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Metadata;

    [AbilityId(AbilityId.techies_remote_mines)]
    public class RemoteMines : CircleAbility
    {
        private readonly SpecialData scepterDamageData;

        public RemoteMines(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.ActivationDelayData = new SpecialData(baseAbility, "detonate_delay");
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.scepterDamageData = new SpecialData(baseAbility, "damage_scepter");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            if (this.Owner.HasAghanimsScepter)
            {
                return new Damage
                {
                    [this.DamageType] = this.scepterDamageData.GetValue(this.Level)
                };
            }

            return base.GetRawDamage(unit, remainingHealth);
        }
    }
}