namespace O9K.Core.Entities.Abilities.Base.Components
{
    using System;

    using Ensage;

    using Entities.Units;

    [Flags]
    public enum AmplifiesDamage
    {
        None = 0,

        Incoming = 1 << 0,

        Outgoing = 1 << 1,

        All = Incoming | Outgoing
    }

    public interface IHasDamageAmplify
    {
        DamageType AmplifierDamageType { get; }

        string[] AmplifierModifierNames { get; }

        AmplifiesDamage AmplifiesDamage { get; }

        bool IsAmplifierAddedToStats { get; }

        bool IsAmplifierPermanent { get; }

        bool IsValid { get; }

        float AmplifierValue(Unit9 source, Unit9 target);
    }
}