namespace O9K.Core.Entities.Abilities.Heroes.Earthshaker
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

    [AbilityId(AbilityId.earthshaker_echo_slam)]
    public class EchoSlam : AreaOfEffectAbility, INuke, IDisable
    {
        private readonly SpecialData echoDamageData;

        private readonly SpecialData searchRadius;

        private Aftershock aftershock;

        public EchoSlam(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "echo_slam_damage_range");
            this.DamageData = new SpecialData(baseAbility, "echo_slam_initial_damage");
            this.echoDamageData = new SpecialData(baseAbility, "echo_slam_echo_damage");
            this.searchRadius = new SpecialData(baseAbility, "echo_slam_echo_search_range");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float Radius
        {
            get
            {
                return base.Radius - 100;
            }
        }

        //public override float Speed { get; } = 550;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage
            {
                [this.DamageType] = this.DamageData.GetValue(this.Level)
            };
            var damageSearchRadius = this.searchRadius.GetValue(this.Level);
            var radius = this.Radius;

            var otherTargets = EntityManager9.Units.Where(
                x => x.IsUnit && !x.Equals(unit) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && x.IsEnemy(this.Owner)
                     && x.Distance(unit) < damageSearchRadius && x.Distance(this.Owner) < radius);

            var multiplier = otherTargets.Sum(x => x.IsHero && !x.IsIllusion ? 2 : 1);
            damage[this.DamageType] += this.echoDamageData.GetValue(this.Level) * multiplier;

            if (this.aftershock?.CanBeCasted() == true && this.Owner.Distance(unit) < this.aftershock.Radius)
            {
                damage += this.aftershock.GetRawDamage(unit);
            }

            return damage;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.earthshaker_aftershock && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                return;
            }

            this.aftershock = (Aftershock)EntityManager9.AddAbility(ability);
        }
    }
}