namespace O9K.Core.Entities.Abilities.Heroes.VengefulSpirit
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.vengefulspirit_command_aura)]
    public class VengeanceAura : PassiveAbility
    {
        public VengeanceAura(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}