namespace O9K.AIO.Heroes.Dynamic.Abilities.Specials
{
    using System;

    using Base;

    using Core.Entities.Abilities.Base;
    using Core.Entities.Abilities.Base.Components.Base;

    internal class SpecialAbilityGroup : OldAbilityGroup<IActiveAbility, OldSpecialAbility>
    {
        public SpecialAbilityGroup(BaseHero baseHero)
            : base(baseHero)
        {
        }

        public override bool AddAbility(Ability9 ability)
        {
            if (!(ability is IActiveAbility type))
            {
                return false;
            }

            if (!this.UniqueAbilities.TryGetValue(ability.Id, out var uniqueType))
            {
                return false;
            }

            var usableAbility = (OldSpecialAbility)Activator.CreateInstance(uniqueType, type);
            usableAbility.AbilitySleeper = this.AbilitySleeper;
            usableAbility.OrbwalkSleeper = this.OrbwalkSleeper;
            this.Abilities.Add(usableAbility);
            this.OrderAbilities();
            return true;
        }
    }
}