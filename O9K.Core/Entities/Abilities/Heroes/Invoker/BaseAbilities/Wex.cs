namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.invoker_wex)]
    public class Wex : ActiveAbility
    {
        public Wex(Ability baseAbility)
            : base(baseAbility)
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

        public string ModifierName { get; } = "modifier_invoker_wex_instance";

        public override bool UseAbility(bool queue = false, bool bypass = false)
        {
            //todo this.ActionSleeper.Sleep(0.03f)?
            return this.BaseAbility.UseAbility(queue, bypass);
        }
    }
}