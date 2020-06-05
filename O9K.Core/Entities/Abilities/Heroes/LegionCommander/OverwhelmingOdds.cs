namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
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

    [AbilityId(AbilityId.legion_commander_overwhelming_odds)]
    public class OverwhelmingOdds : CircleAbility, INuke
    {
        private readonly SpecialData heroDamageData;

        private readonly SpecialData unitDamageData;

        public OverwhelmingOdds(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.heroDamageData = new SpecialData(baseAbility, "damage_per_hero");
            this.unitDamageData = new SpecialData(baseAbility, "damage_per_unit");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);
            var radius = this.Radius;
            var heroDamage = this.heroDamageData.GetValue(this.Level);
            var unitDamage = this.unitDamageData.GetValue(this.Level);

            var otherTargets = EntityManager9.Units.Where(
                x => x.IsUnit && x.IsAlive && !x.IsInvulnerable && !x.IsMagicImmune && x.IsVisible && x.IsEnemy(this.Owner)
                     && x.Distance(unit) < radius);

            damage[this.DamageType] += otherTargets.Sum(x => x.IsHero && !x.IsIllusion ? heroDamage : unitDamage);

            return damage;
        }
    }
}