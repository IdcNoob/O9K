namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.life_stealer_assimilate)]
    public class Assimilate : RangedAbility, IShield
    {
        public Assimilate(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public UnitState AppliesUnitState { get; } = UnitState.Invulnerable;

        public override float CastRange
        {
            get
            {
                return base.CastRange + 100;
            }
        }

        public string ShieldModifierName { get; } = "modifier_life_stealer_assimilate";

        public bool ShieldsAlly { get; } = true;

        public bool ShieldsOwner { get; } = false;
    }
}