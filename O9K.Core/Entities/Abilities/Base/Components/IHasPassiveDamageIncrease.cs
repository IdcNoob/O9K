namespace O9K.Core.Entities.Abilities.Base.Components
{
    using Entities.Units;

    using Helpers.Damage;

    public interface IHasPassiveDamageIncrease
    {
        bool IsPassiveDamagePermanent { get; }

        bool IsValid { get; }

        bool MultipliedByCrit { get; }

        string PassiveDamageModifierName { get; }

        Damage GetRawDamage(Unit9 unit, float? remainingHealth = null);
    }
}