namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.templar_assassin_psionic_trap)]
    public class PsionicTrap : CircleAbility, IDebuff
    {
        public PsionicTrap(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "trap_bonus_damage");
        }

        public string DebuffModifierName { get; } = "modifier_templar_assassin_trap_slow";

        public override float Radius { get; } = 400;
    }
}