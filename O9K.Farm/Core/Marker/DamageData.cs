namespace O9K.Farm.Core.Marker
{
    using System.Collections.Generic;

    using O9K.Core.Entities.Abilities.Base;

    using Units.Base;

    internal class DamageData
    {
        public Dictionary<Ability9, int> AbilityDamage = new Dictionary<Ability9, int>();

        public DamageData(FarmUnit source, IEnumerable<ActiveAbility> abilities, FarmUnit target)
            : this(source, target)
        {
            if (target.IsAlly || target.IsTower)
            {
                return;
            }

            foreach (var ability in abilities)
            {
                this.AbilityDamage.Add(ability, ability.GetDamage(target.Unit));
            }
        }

        public DamageData(FarmUnit source, FarmUnit target)
        {
            if (target.IsTower && target.Unit.HealthPercentage > 10)
            {
                this.AutoAttackDamage = 0;
            }
            else
            {
                this.AutoAttackDamage = source.GetDamage(target);
            }
        }

        public int AutoAttackDamage { get; }
    }
}