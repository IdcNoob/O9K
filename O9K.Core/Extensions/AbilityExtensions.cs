namespace O9K.Core.Extensions
{
    using Ensage;

    using Entities.Abilities.Base;
    using Entities.Abilities.Base.Components;
    using Entities.Abilities.Base.Types;

    public static class AbilityExtensions
    {
        public static bool CanBlock(this IHasDamageBlock block, DamageType damageType)
        {
            return (block.BlockDamageType & damageType) != 0;
        }

        public static bool IsDisable(this Ability9 ability)
        {
            return ability is IDisable;
        }

        public static bool IsDisarm(this IDisable disable, bool hex = true)
        {
            if (!hex && disable.IsHex())
            {
                return false;
            }

            return (disable.AppliesUnitState & UnitState.Disarmed) != 0;
        }

        public static bool IsHex(this IDisable disable)
        {
            return (disable.AppliesUnitState & UnitState.Hexed) != 0;
        }

        public static bool IsIncomingDamageAmplifier(this IHasDamageAmplify amplifier)
        {
            return (amplifier.AmplifiesDamage & AmplifiesDamage.Incoming) != 0;
        }

        public static bool IsInvulnerability(this IDisable disable)
        {
            return (disable.AppliesUnitState & UnitState.Invulnerable) != 0;
        }

        public static bool IsMagicalDamageAmplifier(this IHasDamageAmplify amplifier)
        {
            return (amplifier.AmplifierDamageType & DamageType.Magical) != 0;
        }

        public static bool IsMagicalDamageBlock(this IHasDamageBlock block)
        {
            return (block.BlockDamageType & DamageType.Magical) != 0;
        }

        public static bool IsMagicImmunity(this IShield shield)
        {
            return (shield.AppliesUnitState & UnitState.MagicImmune) != 0;
        }

        public static bool IsMute(this IDisable disable)
        {
            return (disable.AppliesUnitState & UnitState.Muted) != 0;
        }

        public static bool IsOutgoingDamageAmplifier(this IHasDamageAmplify amplifier)
        {
            return (amplifier.AmplifiesDamage & AmplifiesDamage.Outgoing) != 0;
        }

        public static bool IsPhysicalDamageAmplifier(this IHasDamageAmplify amplifier)
        {
            return (amplifier.AmplifierDamageType & DamageType.Physical) != 0;
        }

        public static bool IsPhysicalDamageBlock(this IHasDamageBlock block)
        {
            return (block.BlockDamageType & DamageType.Physical) != 0;
        }

        public static bool IsPureDamageAmplifier(this IHasDamageAmplify amplifier)
        {
            return (amplifier.AmplifierDamageType & DamageType.Pure) != 0;
        }

        public static bool IsPureDamageBlock(this IHasDamageBlock block)
        {
            return (block.BlockDamageType & DamageType.Pure) != 0;
        }

        public static bool IsRoot(this IDisable disable)
        {
            return (disable.AppliesUnitState & UnitState.Rooted) != 0;
        }

        public static bool IsShied(this Ability9 ability)
        {
            return ability is IShield;
        }

        public static bool IsSilence(this IDisable disable, bool hex = true)
        {
            if (!hex && disable.IsHex())
            {
                return false;
            }

            return (disable.AppliesUnitState & UnitState.Silenced) != 0;
        }

        public static bool IsStun(this IDisable disable)
        {
            return (disable.AppliesUnitState & UnitState.Stunned) != 0;
        }
    }
}