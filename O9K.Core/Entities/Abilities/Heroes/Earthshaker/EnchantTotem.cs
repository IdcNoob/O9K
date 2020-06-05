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

    using Prediction.Data;

    [AbilityId(AbilityId.earthshaker_enchant_totem)]
    public class EnchantTotem : CircleAbility, IDisable
    {
        private readonly SpecialData scepterRange;

        private Aftershock aftershock;

        public EnchantTotem(Ability baseAbility)
            : base(baseAbility)
        {
            //todo improve aghs prediction
            this.RadiusData = new SpecialData(baseAbility, "aftershock_range");
            this.scepterRange = new SpecialData(baseAbility, "distance_scepter");
        }

        public override AbilityBehavior AbilityBehavior
        {
            get
            {
                var behavior = base.AbilityBehavior;

                if (this.Owner.HasAghanimsScepter)
                {
                    behavior |= AbilityBehavior.Point;
                }

                return behavior;
            }
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override DamageType DamageType
        {
            get
            {
                if (this.aftershock != null)
                {
                    return this.aftershock.DamageType;
                }

                return base.DamageType;
            }
        }

        public override SkillShotType SkillShotType
        {
            get
            {
                return this.PositionCast ? SkillShotType.Circle : SkillShotType.AreaOfEffect;
            }
        }

        protected override float BaseCastRange
        {
            get
            {
                if (!this.PositionCast)
                {
                    return 0;
                }

                return this.scepterRange.GetValue(this.Level);
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            if (this.aftershock?.CanBeCasted() == true && this.Owner.Distance(unit) < this.aftershock.Radius)
            {
                return this.aftershock.GetRawDamage(unit);
            }

            return new Damage();
        }

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            var result = this.Owner.HasAghanimsScepter
                             ? this.BaseAbility.UseAbility(this.Owner, queue, bypass)
                             : this.BaseAbility.UseAbility(queue, bypass);
            if (result)
            {
                this.ActionSleeper.Sleep(0.1f);
            }

            return result;
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