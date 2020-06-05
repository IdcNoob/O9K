namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.sniper_headshot)]
    public class Headshot : PassiveAbility
    {
        public Headshot(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}