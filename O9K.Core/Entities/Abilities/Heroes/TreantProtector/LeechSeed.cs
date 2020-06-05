namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
    using Base;
    using Base.Types;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.treant_leech_seed)]
    public class LeechSeed : AreaOfEffectAbility, IDebuff
    {
        public LeechSeed(Ability baseAbility)
            : base(baseAbility)
        {
            this.DamageData = new SpecialData(baseAbility, "leech_damage");
            this.RadiusData = new SpecialData(baseAbility, "radius");
        }

        public string DebuffModifierName { get; } = "modifier_treant_leech_seed_slow";
    }
}