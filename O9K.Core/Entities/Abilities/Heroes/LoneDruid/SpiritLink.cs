namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lone_druid_spirit_link)]
    public class SpiritLink : PassiveAbility
    {
        public SpiritLink(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}