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

    [AbilityId(AbilityId.earthshaker_fissure)]
    public class Fissure : LineAbility, IDisable, INuke
    {
        private Aftershock aftershock;

        public Fissure(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "fissure_radius");
        }

        public UnitState AppliesUnitState { get; } = UnitState.Stunned;

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            var damage = base.GetRawDamage(unit, remainingHealth);

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