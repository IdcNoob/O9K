namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
    using System.Linq;

    using Base;
    using Base.Types;

    using Ensage;

    using Entities.Units;

    using Helpers;
    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId(AbilityId.undying_soul_rip)]
    public class SoulRip : RangedAbility, IHealthRestore, INuke
    {
        private readonly SpecialData unitSearchRadiusData;

        public SoulRip(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage_per_unit");
            this.unitSearchRadiusData = new SpecialData(baseAbility, "radius");
        }

        public bool InstantRestore { get; } = true;

        public string RestoreModifierName { get; } = string.Empty;

        public bool RestoresAlly { get; } = true;

        public bool RestoresOwner { get; } = true;

        public int GetHealthRestore(Unit9 unit)
        {
            return (int)this.GetRawDamage(unit)[this.DamageType];
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var multiplier = EntityManager9.Units.ToArray()
                .Count(
                    x => x.IsUnit && !x.Equals(this.Owner) && !x.Equals(unit) && x.IsVisible && x.IsAlive
                         && x.Distance(unit) < this.unitSearchRadiusData.GetValue(this.Level));

            return new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.Level) * multiplier
            };
        }
    }
}