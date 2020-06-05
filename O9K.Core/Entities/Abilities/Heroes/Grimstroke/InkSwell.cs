namespace O9K.Core.Entities.Abilities.Heroes.Grimstroke
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.grimstroke_spirit_walk)]
    public class InkSwell : RangedAbility, IShield
    {
        public InkSwell(Ability baseAbility)
            : base(baseAbility)
        {
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public UnitState AppliesUnitState { get; } = 0;

        public string ShieldModifierName { get; } = "modifier_grimstroke_spirit_walk_buff";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = true;
    }
}