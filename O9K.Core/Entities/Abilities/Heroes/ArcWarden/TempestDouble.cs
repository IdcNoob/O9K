namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.arc_warden_tempest_double)]
    public class TempestDouble : ActiveAbility, IBuff
    {
        public TempestDouble(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = string.Empty;

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}