namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
    using Base;

    using Core.Helpers;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.invoker_exort)]
    public class Exort : ActiveAbility
    {
        private readonly SpecialData orbDamage;

        public Exort(Ability ability)
            : base(ability)
        {
            this.orbDamage = new SpecialData(ability, "bonus_damage_per_instance");
        }

        public int DamagePerOrb
        {
            get
            {
                return (int)this.orbDamage.GetValue(base.Level);
            }
        }

        public override uint Level
        {
            get
            {
                var level = base.Level;
                if (this.Owner.HasAghanimsScepter)
                {
                    level++;
                }

                return level;
            }
        }

        public string ModifierName { get; } = "modifier_invoker_exort_instance";

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            //todo this.ActionSleeper.Sleep(0.03f)?
            return this.BaseAbility.UseAbility(queue, bypass);
        }
    }
}