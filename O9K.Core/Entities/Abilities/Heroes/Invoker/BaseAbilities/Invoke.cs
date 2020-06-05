namespace O9K.Core.Entities.Abilities.Heroes.Invoker.BaseAbilities
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.invoker_invoke)]
    public class Invoke : ActiveAbility
    {
        public Invoke(Ability ability)
            : base(ability)
        {
        }
    }
}