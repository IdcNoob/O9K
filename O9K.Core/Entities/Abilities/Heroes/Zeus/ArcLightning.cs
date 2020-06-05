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

    [AbilityId(AbilityId.zuus_arc_lightning)]
    public class ArcLightning : RangedAbility, INuke
    {
        private StaticField staticField;

        public ArcLightning(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
            this.DamageData = new SpecialData(baseAbility, "arc_damage");
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);

            if (this.staticField?.CanBeCasted() == true)
            {
                damage += this.staticField.GetRawDamage(unit, remainingHealth);
            }

            return damage;
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.zuus_static_field && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                return;
            }

            this.staticField = (StaticField)EntityManager9.AddAbility(ability);
        }
    }
}