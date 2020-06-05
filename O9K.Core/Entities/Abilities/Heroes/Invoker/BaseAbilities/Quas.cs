namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.invoker_quas)]
    public class Quas : ActiveAbility
    {
        public Quas(Ability ability)
            : base(ability)
        {
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

        public string ModifierName { get; } = "modifier_invoker_quas_instance";

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            //todo this.ActionSleeper.Sleep(0.03f)?
            return this.BaseAbility.UseAbility(queue, bypass);
        }
    }
}