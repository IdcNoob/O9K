namespace O9K.Core.Entities.Abilities.Heroes.Viper
{
    using Base;
    using Base.Types;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.viper_poison_attack)]
    public class PoisonAttack : OrbAbility, IHarass
    {
        public PoisonAttack(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override string OrbModifier { get; } = "modifier_viper_poison_attack_slow";
    }
}