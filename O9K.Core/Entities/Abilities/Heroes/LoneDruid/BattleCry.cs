namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.lone_druid_true_form_battle_cry)]
    public class BattleCry : ActiveAbility, IBuff
    {
        public BattleCry(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public string BuffModifierName { get; } = "modifier_lone_druid_true_form_battle_cry";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;
    }
}