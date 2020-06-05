namespace O9K.Core.Entities.Abilities.Heroes.Weaver
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.weaver_time_lapse)]
    public class TimeLapse : ActiveAbility, IShield
    {
        public TimeLapse(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = string.Empty;

        //todo add aghs
        public bool ShieldsAlly { get; } = false;

        public bool ShieldsOwner { get; } = true;
    }
}