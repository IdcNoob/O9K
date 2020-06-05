namespace O9K.Core.Entities.Abilities.Heroes.NightStalker
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.night_stalker_darkness)]
    public class DarkAscension : ActiveAbility, IBuff
    {
        public DarkAscension(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_night_stalker_darkness";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}