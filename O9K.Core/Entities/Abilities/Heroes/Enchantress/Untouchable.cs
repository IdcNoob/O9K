namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
    using Base;

    using Ensage;

    using Helpers;

    using Metadata;

    [AbilityId(AbilityId.enchantress_untouchable)]
    public class Untouchable : PassiveAbility
    {
        private readonly SpecialData attackSpeedSlowData;

        public Untouchable(Ability baseAbility)
            : base(baseAbility)
        {
            this.attackSpeedSlowData = new SpecialData(baseAbility, "slow_attack_speed");
        }

        public float AttackSpeedSlow
        {
            get
            {
                return this.attackSpeedSlowData.GetValue(this.Level);
            }
        }
    }
}