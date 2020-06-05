namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
    using System.Linq;

    using Base;

    using Ensage;

    using Entities.Units;

    using Helpers.Damage;

    using Managers.Entity;

    using Metadata;

    [AbilityId((AbilityId)419)]
    public class SwiftSlash : RangedAbility
    {
        private Omnislash omnislash;

        public SwiftSlash(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override float Radius
        {
            get
            {
                return this.omnislash?.Radius ?? 0;
            }
        }

        public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
        {
            return this.omnislash?.GetRawDamage(unit, remainingHealth) ?? new Damage();
        }

        internal override void SetOwner(Unit9 owner)
        {
            base.SetOwner(owner);

            var ability = EntityManager9.BaseAbilities.FirstOrDefault(
                x => x.Id == AbilityId.juggernaut_omni_slash && x.Owner?.Handle == owner.Handle);

            if (ability == null)
            {
                return;
            }

            this.omnislash = (Omnislash)EntityManager9.AddAbility(ability);
        }
    }
}