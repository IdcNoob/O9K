namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.life_stealer_rage)]
    public class Rage : ActiveAbility, IShield, IBuff
    {
        public Rage(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.MagicImmune;

        public string BuffModifierName { get; } = "modifier_life_stealer_rage";

        public bool BuffsAlly { get; } = false;

        public bool BuffsOwner { get; } = true;

        public string ShieldModifierName { get; } = "modifier_life_stealer_rage";

        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}