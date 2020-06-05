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

    [AbilityId(AbilityId.zuus_thundergods_wrath)]
    public class ThundergodsWrath : AreaOfEffectAbility, INuke
    {
        private StaticField staticField;

        public ThundergodsWrath(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "damage");
        }

        public override float Radius { get; } = 9999999;

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