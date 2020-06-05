namespace O9K.Core.Entities.Abilities.Heroes.Tusk
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.tusk_launch_snowball)]
    public class LaunchSnowball : ActiveAbility
    {
        public LaunchSnowball(Ability baseAbility)
            : base(baseAbility)
        {
        }

        public override string TextureName { get; } = "tusk_snowball";
    }
}