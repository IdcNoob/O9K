namespace O9K.Core.Entities.Abilities.Units.KoboldForeman
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.kobold_taskmaster_speed_aura)]
    public class SpeedAura : PassiveAbility
    {
        public SpeedAura(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}