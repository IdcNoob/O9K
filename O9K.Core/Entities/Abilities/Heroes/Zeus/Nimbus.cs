namespace O9K.Core.Entities.Abilities.Heroes.Zeus
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

    [AbilityId(AbilityId.zuus_cloud)]
    public class Nimbus : CircleAbility, IDisable, INuke
    {
        private LightningBolt lightningBolt;

        private StaticField staticField;

        public Nimbus(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "cloud_radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override float CastRange { get; } = 9999999;

        public override bool IntelligenceAmplify { get; } = false;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = new Damage();

            if (this.staticField?.CanBeCasted() == true)
            {
                damage += this.staticField.GetRawDamage(unit, remainingHealth);
            }

            if (this.lightningBolt?.IsValid == true)
            {
                damage += this.lightningBolt.GetBaseDamage();
            }

            return damage;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.zuus_static_field && x.Owner?.Handle == owner.Handle);

            if (ability != null)
            {
                this.staticField = (StaticField)EntityManager9.AddAbility(ability);
            }

            ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.zuus_lightning_bolt && x.Owner?.Handle == owner.Handle);

            if (ability != null)
            {
                this.lightningBolt = (LightningBolt)EntityManager9.AddAbility(ability);
            }
        }
    }
}