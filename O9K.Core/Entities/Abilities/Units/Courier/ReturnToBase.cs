namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_return_to_base)]
    public class ReturnToBase : ActiveAbility
    {
        public ReturnToBase(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}