namespace O9K.Core.Entities.Abilities.Heroes.Phoenix
{
    using Base;

    using Ensage;

    using Metadata;

    [AbilityId(AbilityId.phoenix_sun_ray_toggle_move)]
    public class SunRayToggleMovement : ActiveAbility
    {
        public SunRayToggleMovement(Ability baseAbility)
            : base(baseAbility)
        {
        }
    }
}