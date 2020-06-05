namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.tiny_grow)]
    public class Grow : PassiveAbility
    {
        public Grow(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}