namespace O9K.Core.Entities.Abilities.Units.Courier
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.courier_shield)]
    public class Shield : ActiveAbility
    {
        public Shield(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}